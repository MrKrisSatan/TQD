using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace QuietDread {
public partial class Game {
 bool musicTesting;
 IEnumerator MusicSmoke(){
  musicTesting=true;Directory.CreateDirectory(qaDir);var lines=new List<string>();var failures=new List<string>();Application.logMessageReceived+=(text,stack,type)=>{if(type==LogType.Error||type==LogType.Exception||type==LogType.Assert)failures.Add(text);};
  Action<bool,string> check=(ok,n)=>{lines.Add((ok?"PASS ":"FAIL ")+n);if(!ok)failures.Add(n);};
  yield return new WaitForSeconds(1);music.SetEnabled(true);music.Volume=.5f;AudioListener.volume=0;
  foreach(var context in new[]{"menu","trail","conquest","battle"}){
   music.SetContext(context);yield return new WaitForSeconds(3);var source=music.ActiveSource;check(source!=null&&source.isPlaying,context+" plays streamed audio");check(source!=null&&source.ignoreListenerVolume&&source.volume>.4f,context+" is independent of muted effects");
   if(source&&source.clip){source.time=Mathf.Min(30,source.clip.length*.2f);var samples=new float[1024];float peak=0;for(int sample=0;sample<20;sample++){yield return new WaitForSeconds(.1f);source.GetOutputData(samples,0);foreach(float s in samples)peak=Mathf.Max(peak,Mathf.Abs(s));}lines.Add("INFO "+context+" length="+source.clip.length+" time="+source.time+" load="+source.clip.loadState+" virtual="+source.isVirtual);check(peak>.000001f,context+" decoded audio signal (peak "+peak.ToString("F6")+")");float before=source.time;music.SetContext(context);yield return new WaitForSeconds(.2f);check(music.ActiveSource==source&&source.time>=before,context+" unchanged context does not restart track");}
   check(music.PlayingCount<=2,context+" bounded crossfade sources");
  }
  var first=music.ActiveSource;first.time=first.clip.length-.25f;yield return new WaitForSeconds(3);check(music.Track=="battle-trailer"&&music.ActiveSource.isPlaying,"battle playlist advances to second war track");music.ActiveSource.time=15;yield return new WaitForSeconds(.8f);var wave=new float[1024];music.ActiveSource.GetOutputData(wave,0);float max=0;foreach(float s in wave)max=Mathf.Max(max,Mathf.Abs(s));check(max>.000001f,"second war track produces decoded signal");
  music.SetEnabled(false);yield return new WaitForSeconds(2.3f);check(music.PlayingCount==0,"music toggle fades and stops both sources");music.SetEnabled(true);yield return new WaitForSeconds(2.3f);check(music.ActiveSource.isPlaying,"music resumes after enabling");music.Volume=0;yield return null;check(music.ActiveSource.volume==0,"music slider can mute independently");music.Volume=.45f;AudioListener.volume=volume;music.SetContext("menu");showSettings=true;yield return new WaitForSeconds(2.3f);ScreenCapture.CaptureScreenshot(Path.Combine(qaDir,"27-music-settings.png"));yield return new WaitForSeconds(.8f);
  File.WriteAllText(Path.Combine(qaDir,"music-runtime.txt"),(failures.Count==0?"PASS":"FAIL")+": music runtime checks\n"+string.Join("\n",lines)+"\n"+string.Join("\n",failures));Application.Quit(failures.Count==0?0:1);
 }
}
}
