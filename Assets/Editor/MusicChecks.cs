using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QuietDread;
public class MusicImport:AssetPostprocessor {
 void OnPreprocessAudio(){if(!assetPath.StartsWith("Assets/Resources/Music/"))return;var importer=(AudioImporter)assetImporter;var settings=importer.defaultSampleSettings;settings.loadType=AudioClipLoadType.Streaming;settings.compressionFormat=AudioCompressionFormat.Vorbis;settings.quality=.7f;importer.defaultSampleSettings=settings;importer.loadInBackground=true;importer.forceToMono=false;}
}
public static class MusicChecks {
 public static void Run(string root){var lines=new List<string>();Action<bool,string> check=(ok,n)=>{if(!ok)throw new Exception("MUSIC CHECK: "+n);lines.Add("PASS "+n);};foreach(var id in MusicController.TrackIds){var clip=Resources.Load<AudioClip>("Music/"+id);check(clip!=null&&clip.length>30&&clip.channels>=1,"valid imported track "+id);check(clip.loadType==AudioClipLoadType.Streaming,"streaming import "+id);}
  check(MusicController.Route("menu","trail",false)=="menu","menu overrides saved campaign");check(MusicController.Route("campaign","trail",false)=="trail","Trail atmosphere");check(MusicController.Route("campaign","conquest",false)=="conquest","Conquest atmosphere");check(MusicController.Route("field","trail",false)=="battle","battle preparation music");check(MusicController.Route("battle","trail",false)=="battle","live battle playlist");check(MusicController.Route("battle","trail",true)=="trail","results return to campaign atmosphere");check(MusicController.Playlist("battle").Length==2,"both war tracks in playlist");
  File.WriteAllText(Path.Combine(root,"Documentation/MUSIC-TEST-RESULTS.txt"),lines.Count+" music import and routing checks passed\n"+string.Join("\n",lines));
 }
}
