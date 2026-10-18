using System;
using System.Linq;
using UnityEngine;
namespace QuietDread {
public class MusicController:MonoBehaviour {
 public const float FadeSeconds=2f;
 public static readonly string[] TrackIds={"menu","trail","conquest","battle-epic","battle-trailer"};
 AudioClip[] clips;AudioSource[] players;readonly float[] gains={0,0};int incoming=-1,playlistIndex;string context="";float startedAt;
 public float Volume=.45f;public bool Enabled=true;
 public string Context {get{return context;}}
 public string Track {get{return incoming<0||players[incoming].clip==null?"":players[incoming].clip.name;}}
 public AudioSource ActiveSource {get{return incoming<0?null:players[incoming];}}
 public int PlayingCount {get{return players.Count(p=>p.isPlaying);}}
 public static string Route(string screen,string campaignMode,bool finished){if(screen=="field"||screen=="battle"&&!finished)return "battle";if((screen=="campaign"||screen=="battle")&&(campaignMode=="trail"||campaignMode=="conquest"))return campaignMode;return "menu";}
 public static string[] Playlist(string context){return context=="battle"?new[]{"battle-epic","battle-trailer"}:new[]{context=="trail"?"trail":context=="conquest"?"conquest":"menu"};}
 void Awake(){Volume=Mathf.Clamp01(PlayerPrefs.GetFloat("musicVolume",.45f));Enabled=PlayerPrefs.GetInt("musicEnabled",1)==1;clips=TrackIds.Select(id=>Resources.Load<AudioClip>("Music/"+id)).ToArray();players=new AudioSource[2];for(int i=0;i<2;i++){players[i]=gameObject.AddComponent<AudioSource>();players[i].playOnAwake=false;players[i].spatialBlend=0;players[i].ignoreListenerVolume=true;players[i].volume=0;players[i].loop=false;}for(int i=0;i<clips.Length;i++)if(!clips[i])Debug.LogWarning("Music asset missing: "+TrackIds[i]);}
 public void SetContext(string value){if(context==value)return;context=value;playlistIndex=0;if(Enabled)PlayCurrent();}
 public void SetEnabled(bool value){if(Enabled==value)return;Enabled=value;if(value)PlayCurrent();}
 AudioClip CurrentClip(){var list=Playlist(context);return clips[Array.IndexOf(TrackIds,list[playlistIndex%list.Length])];}
 void PlayCurrent(){var clip=CurrentClip();if(!clip)return;for(int i=0;i<2;i++)if(players[i].clip==clip&&players[i].isPlaying){incoming=i;return;}
  int next=incoming<0?0:gains[0]<=gains[1]?0:1;players[next].Stop();gains[next]=0;players[next].volume=0;players[next].clip=clip;players[next].Play();incoming=next;startedAt=Time.unscaledTime;
 }
 void Update(){if(players==null)return;if(Enabled&&context!=""&&Time.unscaledTime-startedAt>.5f){var p=ActiveSource;if(p==null||!p.isPlaying||p.clip.length-p.time<=Mathf.Min(FadeSeconds,p.clip.length*.25f)){playlistIndex=(playlistIndex+1)%Playlist(context).Length;var clip=CurrentClip();if(p!=null&&p.clip==clip){int next=1-incoming;players[next].Stop();gains[next]=0;players[next].volume=0;players[next].clip=clip;players[next].Play();incoming=next;}else PlayCurrent();}}
  for(int i=0;i<2;i++){float goal=Enabled&&i==incoming?1:0;gains[i]=Mathf.MoveTowards(gains[i],goal,Time.unscaledDeltaTime/FadeSeconds);players[i].volume=gains[i]*Mathf.Clamp01(Volume);if(goal==0&&gains[i]<=0&&players[i].isPlaying)players[i].Stop();}
 }
 public void SavePreferences(){PlayerPrefs.SetFloat("musicVolume",Volume);PlayerPrefs.SetInt("musicEnabled",Enabled?1:0);}
 void OnDestroy(){if(players!=null)foreach(var p in players)if(p)p.Stop();}
}
public partial class Game {
 MusicController music;
 void InitMusic(){music=gameObject.AddComponent<MusicController>();music.SetContext("menu");}
 void UpdateMusic(){if(music&&!musicTesting)music.SetContext(MusicController.Route(screen,campaign?.mode,battle!=null&&battle.finished));}
}
}

