using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
namespace QuietDread {
[Serializable] public class NarratorSettings {public bool enabled,longer;public string endpoint="http://127.0.0.1:11434",model="",tone="solemn";}
public class NarrativeController:MonoBehaviour {
 public NarratorSettings settings=new NarratorSettings();public Campaign Bound{get;private set;}public string status="Narrator disabled. Authored chronicle remains available.";public LocalModel[] models=new LocalModel[0];public bool busy,testing;public Action Changed;
 CancellationTokenSource active,discovery;ChronicleEvent activeEvent;int epoch;bool shuttingDown;
 public string LastRawReply;public float requestTimeoutSeconds=90;public INarrativeProvider ProviderOverride; // Test seam; never selected by model output.
 void Awake(){try{settings=JsonUtility.FromJson<NarratorSettings>(PlayerPrefs.GetString("narrator-settings",""))??new NarratorSettings();}catch{settings=new NarratorSettings();}}
 public void StoreSettings(){Cancel("Settings changed; authored text retained.");discovery?.Cancel();PlayerPrefs.SetString("narrator-settings",JsonUtility.ToJson(settings));PlayerPrefs.Save();models=new LocalModel[0];status=settings.enabled?"Ready. Test connection to discover local models.":"Narrator disabled; authored chronicle active.";}
 public void Bind(Campaign campaign){if(ReferenceEquals(Bound,campaign))return;Cancel("Campaign changed; previous request cancelled.");Bound=campaign;if(Bound!=null){Bound.EnsureChronicle();foreach(var e in Bound.journal.Where(x=>x.generation=="generating"))e.generation="interrupted";}}
 INarrativeProvider Provider(){return ProviderOverride??new OllamaNarrativeProvider(new LoopbackTransport(settings.endpoint));}
 public async void Discover(){if(testing)return;testing=true;var request=new CancellationTokenSource(TimeSpan.FromSeconds(8));discovery=request;status="Connecting to local Ollama…";try{var found=await Provider().Models(request.Token);if(request.IsCancellationRequested)return;models=found;if(found.Length==0)status="Ollama is running, but no installed local text models were found. Cloud models are excluded.";else{if(!found.Any(m=>m.name==settings.model))settings.model=found[0].name;status=found.Length+" installed local text models available. Select one, then Apply.";}}catch(Exception e){if(!shuttingDown&&!request.IsCancellationRequested)status="Connection test failed: "+Friendly(e);}finally{testing=false;if(ReferenceEquals(discovery,request))discovery=null;request.Dispose();}}
 void Update(){if(Bound==null||busy||!settings.enabled||string.IsNullOrEmpty(settings.model)||shuttingDown)return;var pending=Bound.journal.Where(e=>e.generation=="pending").ToList();if(pending.Count>4){foreach(var e in pending.Take(pending.Count-4))e.generation="backlog skipped";Changed?.Invoke();}var next=pending.Skip(Math.Max(0,pending.Count-4)).FirstOrDefault();if(next!=null)Generate(next);}
 public void Retry(ChronicleEvent e){if(Bound==null||!Bound.journal.Contains(e))return;Cancel("Previous request skipped.");e.generation="pending";Changed?.Invoke();}
 public void RestoreFallback(ChronicleEvent e){if(e==activeEvent)Cancel("Generation skipped.");e.text=NarrativeText.Fallback(e);e.source="Authored fallback";e.generation="skipped";Changed?.Invoke();}
 public void Cancel(string reason="Generation skipped; authored text retained."){epoch++;active?.Cancel();active=null;if(activeEvent!=null&&activeEvent.generation=="generating")activeEvent.generation="skipped";activeEvent=null;busy=false;status=reason;}
 public async void Generate(ChronicleEvent entry){if(busy||Bound==null||!Bound.journal.Contains(entry)||!settings.enabled)return;var owner=Bound;int stamp=epoch;var request=new CancellationTokenSource();active=request;activeEvent=entry;busy=true;entry.generation="generating";status="Writing chronicle entry "+entry.number+" locally…";
  try{var provider=Provider();string prompt=StoryValidation.Prompt(owner,entry,settings.longer);string model=settings.model,tone=settings.tone;int tokens=settings.longer?650:420;StoryReply validated=null;for(int attempt=0;attempt<2;attempt++){try{using(var timeout=CancellationTokenSource.CreateLinkedTokenSource(request.Token)){timeout.CancelAfter(TimeSpan.FromSeconds(requestTimeoutSeconds));string raw=await provider.Generate(model,prompt,tone,tokens,timeout.Token);LastRawReply=NarrativeText.Limit(raw,12000);validated=StoryValidation.Parse(raw,entry);}break;}catch(Exception failure)when(attempt==0&&!request.IsCancellationRequested){if(failure is System.IO.InvalidDataException)prompt+="\nCorrection for retry: return the exact factual contract and a text passage of 45–90 words containing no numerical claims except exact recorded location names, and no markup, links, commands or new events. "+failure.Message;await Task.Delay(500,request.Token);}}
   if(!CanCommit(owner,entry,stamp,request.Token))return;entry.text=validated.text;entry.thread=validated.thread;entry.source="Local AI · "+model;entry.generation="complete";status="Chronicle entry "+entry.number+" complete. Generated atmosphere is not canon.";Changed?.Invoke();
  }catch(Exception e){if(CanCommit(owner,entry,stamp,request.Token)){entry.generation="fallback";entry.text=NarrativeText.Fallback(entry);entry.source="Authored fallback";status="Authored passage retained: "+Friendly(e);Changed?.Invoke();}}
  finally{if(epoch==stamp){active=null;activeEvent=null;busy=false;}request.Dispose();}
 }
 public bool CanCommit(Campaign owner,ChronicleEvent entry,int stamp,CancellationToken token){return !shuttingDown&&!token.IsCancellationRequested&&stamp==epoch&&ReferenceEquals(owner,Bound)&&owner.journal.Contains(entry)&&entry.generation=="generating";}
 static string Friendly(Exception e){return e is OperationCanceledException?"Request cancelled or timed out.":NarrativeText.Limit(e.Message,240);}
 void OnDestroy(){shuttingDown=true;Cancel();discovery?.Cancel();}
}
}




