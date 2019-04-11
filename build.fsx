#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.Core.Target //"

#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.IO

let artifactsDir = "./artifacts"

Target.create "Cleanup" (fun _ ->
    Shell.cleanDir artifactsDir
)

Target.create "Build Toolkit" (fun _ ->
    "./Anapher.Wpf.Toolkit" |> DotNet.pack (fun opts -> {opts with Configuration = DotNet.BuildConfiguration.Release
                                                                   OutputPath = Some artifactsDir
                                                              })
)

Target.create "Build Toolkit.Metro" (fun _ ->
    "./Anapher.Wpf.Toolkit.Metro" |> DotNet.pack (fun opts -> {opts with Configuration = DotNet.BuildConfiguration.Release
                                                                         OutputPath = Some artifactsDir
                                                              })
)

Target.create "All" ignore

open Fake.Core.TargetOperators

"Cleanup"
  ==> "Build Toolkit"
  ==> "All"

"Cleanup"
  ==> "Build Toolkit.Metro"
  ==> "All"

Target.runOrDefault "All"