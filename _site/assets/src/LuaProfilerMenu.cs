using UnityEditor;
using Spark;
static class LuaProfilerMenu{
	[MenuItem("SLua/Attach Lua Profiler")]
	private static void Attach(){
		if(EditorApplication.isPlaying){
			SparkLua.G.doString(@"
				local Profiler=UnityEngine.Profiling.Profiler
				local debug=debug
				local cache={}
				debug.sethook(function(event,line)
					if event=='call' then
						local func=debug.getinfo(2,'f').func
						local name=cache[func]
						if not name then
							local info=debug.getinfo(2,'Sn')
							name=info.short_src
							if info.name then
								name=name..'.'..info.name
							end
							if info.linedefined>0 then
								name=name..':'..info.linedefined
							end
							cache[func]=name
						end
						Profiler.BeginSample(name)
					elseif event=='return' then
						Profiler.EndSample()
					end
				end,'cr',0)
			");
			EditorWindow.GetWindow(typeof(Editor).Assembly.GetType("UnityEditor.ProfilerWindow"));
		}
	}
}
