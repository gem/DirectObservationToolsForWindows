Imports System.ComponentModel
Imports System.Configuration.Install

Public Class Install

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent.
    End Sub

    Public Overrides Sub Install(ByVal stateSaver As System.Collections.IDictionary)
        MyBase.Install(stateSaver)

        Dim path As String = Me.Context.Parameters.Item("arg1")

        Dim winpath As String = Environment.GetEnvironmentVariable("windir")
        Dim vcinstalled As Boolean = False
        For Each dircheck In System.IO.Directory.GetDirectories(winpath & "\WinSxS\")
            If UCase(dircheck).Contains("X86_MICROSOFT.VC90.CRT_") And dircheck.Contains("9.0.30729") Then
                vcinstalled = True
                Exit For
            End If
        Next
        If Not vcinstalled Then
            Throw New System.Configuration.Install.InstallException("Microsoft Visual C++ Redistributable 2008 is not installed. Please install this and try again.")
        End If

        System.Environment.SetEnvironmentVariable("PROJ_LIB", path + "PROJ_NAD", System.EnvironmentVariableTarget.Machine)
        System.Environment.SetEnvironmentVariable("GDAL_DATA", path + "gdal_data", System.EnvironmentVariableTarget.Machine)

        Dim exitCode1 As Integer = ExecuteCommand("regsvr32", " /u /s " + """" + path & "MapWinGIS.ocx" + """", 120000)
        Dim exitCode2 As Integer = ExecuteCommand("regsvr32", " /s " + """" + path & "MapWinGIS.ocx" + """", 120000)


    End Sub

    Public Overrides Sub Uninstall(ByVal savedState As System.Collections.IDictionary)
        MyBase.Uninstall(savedState)

        Dim path As String = Me.Context.Parameters.Item("arg1")
        Dim exitCode1 As Integer = ExecuteCommand("regsvr32", " /u /s " + """" + path & "MapWinGIS.ocx" + """", 120000)

    End Sub


    Private Function ExecuteCommand(ByVal Command1 As String, ByVal Command2 As String, ByVal Timeout As Integer) As Integer

        'Set up a ProcessStartInfo using your path to the executable (Command1) and the command line arguments (Command2).
        Dim ProcessInfo As ProcessStartInfo = New ProcessStartInfo(Command1, Command2)
        ProcessInfo.CreateNoWindow = True
        ProcessInfo.UseShellExecute = False

        'Invoke the process.
        Dim Process As Process = Process.Start(ProcessInfo)
        Process.WaitForExit(Timeout)

        'Finish.
        Dim ExitCode As Integer = Process.ExitCode
        Process.Close()
        Return ExitCode
    End Function


End Class
