Imports System.Reflection

Module App
    Friend ReadOnly Property Path() As String
        Get
            Dim tStr As String = System.Windows.Forms.Application.ExecutablePath
            Return Left(tStr, tStr.LastIndexOf("\"))
        End Get
    End Property

    Friend ReadOnly Property Major() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileMajorPart
        End Get
    End Property

    Friend ReadOnly Property Minor() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileMinorPart
        End Get
    End Property

    Friend ReadOnly Property Revision() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileBuildPart
        End Get
    End Property
    ''' <summary>
    ''' Gets the version number including the build number
    ''' </summary>
    ''' <returns>The version number</returns>
    ''' <remarks>Paul Meems, June 22, 2011</remarks>
    Friend ReadOnly Property AssemblyVersion() As String
        Get
            Return Assembly.GetEntryAssembly().GetName().Version.ToString()
        End Get
    End Property

    ''' <summary>
    ''' Gets the assembly copyright.
    ''' </summary>
    ''' <returns>The assembly copyright.</returns>
    ''' <remarks>Paul Meems, June 22, 2011</remarks>
    Public ReadOnly Property AssemblyCopyright() As String
        Get
            ' Get all Copyright attributes on this assembly
            Dim attributes As Object() = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)
            ' If there aren't any Copyright attributes, return an empty string
            If attributes.Length = 0 Then
                Return ""
            End If
            ' If there is a Copyright attribute, return its value
            Return DirectCast(attributes(0), AssemblyCopyrightAttribute).Copyright
        End Get
    End Property

    Friend ReadOnly Property VersionString() As String
        Get
            Dim vi As System.Diagnostics.FileVersionInfo
            Dim versionStr As String
            vi = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
            versionStr = vi.FileMajorPart & "." & vi.FileMinorPart & "." & vi.FileBuildPart
            Return versionStr
        End Get
    End Property

End Module
