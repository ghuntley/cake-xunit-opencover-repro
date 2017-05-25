#tool "coveralls.io"
#tool "OpenCover"
#tool "ReportGenerator"

Task("Default")
    .Does(() =>
{
    var artifactDirectory = "./artifacts/";
    var testCoverageOutputFile = artifactDirectory + "OpenCover.xml";

    NuGetRestore("./cakerepro.sln");

    MSBuild("./cakerepro.sln", new MSBuildSettings()
        .SetConfiguration("Release")
        .WithProperty("TreatWarningsAsErrors", "true")
        .SetVerbosity(Verbosity.Minimal)
        .SetNodeReuse(false));


    Action<ICakeContext> testAction = tool => {

        tool.XUnit2("./silentFailure/bin/Release/silentFailure.dll", new XUnit2Settings {
            OutputDirectory = artifactDirectory,
            XmlReportV1 = true,
            NoAppDomain = true
        });
    };

    OpenCover(testAction,
        testCoverageOutputFile,
        new OpenCoverSettings {
            ReturnTargetCodeOffset = 0,
            ArgumentCustomization = args => args.Append("-mergeoutput")
        });

    ReportGenerator(testCoverageOutputFile, artifactDirectory);
});

RunTarget("Default");