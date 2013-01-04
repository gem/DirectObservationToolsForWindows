'********************************************************************************************************
'File Name: clsAppInfo.vb
'Description: Friend class stores variables associated with the current MapWindow configuration.  
'These variables provide customization for the MapWindow Application interface.   
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source. 
'
'The Initial Developer of this version of the Original Code is Daniel P. Ames using portions created by 
'Utah State University and the Idaho National Engineering and Environmental Lab that were released as 
'public domain in March 2004.  
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'1/1/2005 - dpa - Updated 
'19/9/2005 - Lailin Chen - Changed the default path
'3/31/2008 - Jiri Kadlec - Added 'OverrideSystemLocale' and 'CustomLocale' properties
'5/10/2008 - Jiri kadlec - Added 'DefaultViewBackColor' property
'7/28/2011 - Teva - Added MapWindowVersion property
'7/28/2011 - Teva - Added Floating Scalebar property
'8/1/2011 - Teva - Added the show/hide redraw label speed property
'8/3/2011 - Teva - Added the Avoid collision property
'8/4/2011 - Teva - Added the Auto Create Spatial Index property
'********************************************************************************************************

Friend Class cAppInfo

    Implements MapWindow.Interfaces.AppInfo

    Public Version As String
    Public BuildDate As String
    Public Developer As String
    Public Comments As String

    Private _Name As String
    Private _HelpFilePath As String
    Private _WelcomePlugin As String
    Private _SplashPicture As Image
    Private _FormIcon As Icon
    Private _SplashTime As Double
    Private _DefaultDir As String
    Private _URL As String
    Private _ShowWelcomeScreen As Boolean
    Private m_useSplashScreen As Boolean
    Private _ShowMapWindowVersion As Boolean
    Private _ShowFloatingScalebar As Boolean
    Private _ShowHideRedrawSpeed As Boolean
    Private _ShowAvoidCollision As Boolean
    Private _ShowAutoCreateSpatialindex As Boolean

    Public ApplicationPluginDir As String
    Public m_neverShowProjectionDialog As Boolean
    Public ProjectionDialog_PreviousNoProjAnswer As String
    Public ProjectionDialog_PreviousMismatchAnswer As String
    Public LoadTIFFandIMGasgrid As GeoTIFFAndImgBehavior = GeoTIFFAndImgBehavior.Automatic ' Default=auto
    Public LoadESRIAsGrid As ESRIBehavior = ESRIBehavior.LoadAsGrid
    Public MouseWheelZoom As MouseWheelZoomDir = MouseWheelZoomDir.WheelUpZoomsIn
    Public LogfilePath As String = ""
    Public LabelsUseProjectLevel As Boolean

    'Default map background color - added by jk 5/10/2008
    Public DefaultBackColor As Color = Color.FromArgb(0, 255, 255, 255)

    'User-defined Language Settings (added by jk 3/31/2008)
    Public OverrideSystemLocale As Boolean = False
    Public Locale As String = String.Empty

    'Distance Measuring Stuff:
    Public MeasuringCurrently As Boolean
    Public MeasuringStartX As Double
    Public MeasuringStartY As Double
    Public MeasuringScreenPointStart As System.Drawing.Point
    Public MeasuringScreenPointFinish As System.Drawing.Point
    Public MeasuringTotalDistance As Double
    Public MeasuringDrawing As Integer
    Public MeasuringPreviousSegments As ArrayList

    'Area Measuring Stuff:
    Public AreaMeasuringCurrently As Boolean
    Public AreaMeasuringlstDrawPoints As New ArrayList
    Public AreaMeasuringReversibleDrawn As New ArrayList
    Public AreaMeasuringLastStartPtX As Double = -1
    Public AreaMeasuringLastStartPtY As Double = -1
    Public AreaMeasuringStartPtX As Double = -1
    Public AreaMeasuringStartPtY As Double = -1
    Public AreaMeasuringLastEndX As Double = -1
    Public AreaMeasuringLastEndY As Double = -1
    Public AreaMeasuringEraseLast As Boolean = False
    Public AreaMeasuringmycolor As New System.Drawing.Color

    Private MeasureCursor As Cursor = Nothing
    'Dynamic visibility application properties 12/10/2008 ARA
    Public ShowDynamicVisibilityWarnings As Boolean = True
    Public ShowLayerAfterDynamicVisibility As Boolean = True
    Public m_symbologyLoadingBehavior As SymbologyBehavior
    Public m_projectionAbsenceBehavior As ProjectionAbsenceBehavior
    Public m_projectionMismatchBehavior As ProjectionMismatchBehavior
    Private m_showLoadingReport As Boolean
    Public ProjectReloading As Boolean

    ' list of favorite projections (EPSG codes)
    Private m_favoriteProjections As Generic.List(Of Integer) = Nothing

    Public Sub New()
        'Initialization function to provide initital values to the 
        'key application variables
        Dim path As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(Me.GetType).Location)
        _Name = "MapWindow GIS"
        Version = App.VersionString
        ApplicationPluginDir = App.Path + "\" + "ApplicationPlugins"
        Developer = "MapWindow OSS Team"
        _SplashTime = 2
        _URL = "http://www.mapwindow.org"
        _ShowWelcomeScreen = True
        _ShowMapWindowVersion = True
        _HelpFilePath = path & "\help\MapWindow.chm"
        DefaultDir = path 'Changed by Lailin Chen
        m_neverShowProjectionDialog = False ' Default value
        ProjectionDialog_PreviousNoProjAnswer = ""
        ProjectionDialog_PreviousMismatchAnswer = ""
        m_showLoadingReport = True

        OverrideSystemLocale = False
        Locale = String.Empty

        m_useSplashScreen = True
        ProjectReloading = False

        m_symbologyLoadingBehavior = SymbologyBehavior.DefaultOptions
        m_projectionAbsenceBehavior = Interfaces.ProjectionAbsenceBehavior.AssignFromProject
        m_projectionMismatchBehavior = Interfaces.ProjectionMismatchBehavior.Reproject

        m_favoriteProjections = New Generic.List(Of Integer)
        m_favoriteProjections.Add(4326)  ' WGS84
        m_favoriteProjections.Add(3857)  ' WGS 84 / Pseudo Mercator
    End Sub

    ''' <summary>
    ''' Determines whether projection mismatch report should be shown when loading layers
    ''' </summary>
    Public Property ShowLoadingReport() As Boolean Implements Interfaces.AppInfo.ShowLoadingReport
        Get
            Return m_showLoadingReport
        End Get
        Set(ByVal value As Boolean)
            m_showLoadingReport = value
        End Set
    End Property

    ''' <summary>
    ''' Prevents the displaying of projection mismatch dialog
    ''' </summary>
    Public Property NeverShowProjectionDialog() As Boolean Implements Interfaces.AppInfo.NeverShowProjectionDialog
        Get
            Return m_neverShowProjectionDialog
        End Get
        Set(ByVal value As Boolean)
            m_neverShowProjectionDialog = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets projection absence behavior
    ''' </summary>
    Public Property ProjectionAbsenceBehavior() As Interfaces.ProjectionAbsenceBehavior Implements Interfaces.AppInfo.ProjectionAbsenceBehavior
        Get
            Return m_projectionAbsenceBehavior
        End Get
        Set(ByVal value As Interfaces.ProjectionAbsenceBehavior)
            m_projectionAbsenceBehavior = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets projection mismatch behavior
    ''' </summary>
    Public Property ProjectionMismatchBehavior() As Interfaces.ProjectionMismatchBehavior Implements Interfaces.AppInfo.ProjectionMismatchBehavior
        Get
            Return m_projectionMismatchBehavior
        End Get
        Set(ByVal value As Interfaces.ProjectionMismatchBehavior)
            m_projectionMismatchBehavior = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the list of favorite projections
    ''' </summary>
    Public ReadOnly Property FavoriteProjections() As System.Collections.Generic.List(Of Integer) Implements Interfaces.AppInfo.FavoriteProjections
        Get
            Return m_favoriteProjections
        End Get
    End Property

    Public Property SymbologyLoadingBehavior() As Interfaces.SymbologyBehavior Implements Interfaces.AppInfo.SymbologyLoadingBehavior
        Get
            Return m_symbologyLoadingBehavior
        End Get
        Set(ByVal value As MapWindow.Interfaces.SymbologyBehavior)
            m_symbologyLoadingBehavior = value
        End Set
    End Property

    Public Property DefaultDir() As String Implements Interfaces.AppInfo.DefaultDir
        Get
            Return _DefaultDir
        End Get
        Set(ByVal Value As String)
            _DefaultDir = Value
        End Set
    End Property

    Public Property FormIcon() As System.Drawing.Icon Implements Interfaces.AppInfo.FormIcon
        Get
            Return _FormIcon
        End Get
        Set(ByVal Value As System.Drawing.Icon)
            _FormIcon = Value
        End Set
    End Property

    Public Property HelpFilePath() As String Implements Interfaces.AppInfo.HelpFilePath
        Get
            Return _HelpFilePath
        End Get
        Set(ByVal Value As String)
            _HelpFilePath = Value
        End Set
    End Property

    Public Property ShowWelcomeScreen() As Boolean Implements Interfaces.AppInfo.ShowWelcomeScreen
        Get
            Return _ShowWelcomeScreen
        End Get
        Set(ByVal Value As Boolean)
            _ShowWelcomeScreen = Value
        End Set
    End Property

    Public Property ShowAvoidCollision() As Boolean
        Get
            Return _ShowAvoidCollision
        End Get
        Set(ByVal Value As Boolean)
            _ShowAvoidCollision = Value
        End Set
    End Property

    Public Property ShowAutoCreateSpatialindex() As Boolean
        Get
            Return _ShowAutoCreatespatialindex
        End Get
        Set(ByVal Value As Boolean)
            _ShowAutoCreatespatialindex = Value
        End Set
    End Property

    Public Property ShowMapWindowVersion() As Boolean Implements Interfaces.AppInfo.ShowMapWindowVersion
        Get
            Return _ShowMapWindowVersion
        End Get
        Set(ByVal Value As Boolean)
            _ShowMapWindowVersion = Value
        End Set
    End Property

    Public Property ShowHideRedrawSpeed() As Boolean Implements Interfaces.AppInfo.ShowRedrawSpeed
        Get
            Return _ShowHideRedrawSpeed
        End Get
        Set(ByVal Value As Boolean)
            _ShowHideRedrawSpeed = Value
        End Set
    End Property


    Public Property ShowFloatingScalebar() As Boolean Implements Interfaces.AppInfo.ShowFloatingScalebar
        Get
            Return _ShowFloatingScalebar
        End Get
        Set(ByVal Value As Boolean)
            _ShowFloatingScalebar = Value
        End Set
    End Property

    Public Property UseSplashScreen() As Boolean Implements Interfaces.AppInfo.UseSplashScreen
        Get
            Return m_useSplashScreen
        End Get
        Set(ByVal Value As Boolean)
            m_useSplashScreen = Value
        End Set
    End Property

    Public Property SplashPicture() As System.Drawing.Image Implements Interfaces.AppInfo.SplashPicture
        Get
            Return _SplashPicture
        End Get
        Set(ByVal Value As System.Drawing.Image)
            _SplashPicture = Value
        End Set
    End Property

    Public Property SplashTime() As Double Implements Interfaces.AppInfo.SplashTime
        Get
            Return _SplashTime
        End Get
        Set(ByVal Value As Double)
            _SplashTime = Value
        End Set
    End Property

    Public Property URL() As String Implements Interfaces.AppInfo.URL
        Get
            Return _URL
        End Get
        Set(ByVal Value As String)
            _URL = Value
        End Set
    End Property

    Public Property WelcomePlugin() As String Implements Interfaces.AppInfo.WelcomePlugin
        Get
            Return _WelcomePlugin
        End Get
        Set(ByVal Value As String)
            _WelcomePlugin = Value
        End Set
    End Property

    Public Property Name() As String Implements Interfaces.AppInfo.ApplicationName
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property

#Region "Measuring tools"
    ' TODO: comments are needed
    Friend Sub AreaMeasuringClearTempLines()
        If (Me.AreaMeasuringEraseLast) Then
            System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(Me.AreaMeasuringLastStartPtX, Me.AreaMeasuringLastStartPtY), New System.Drawing.Point(Me.AreaMeasuringLastEndX, Me.AreaMeasuringLastEndY), Me.AreaMeasuringmycolor)
            System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(Me.AreaMeasuringStartPtX, Me.AreaMeasuringStartPtY), New System.Drawing.Point(Me.AreaMeasuringLastEndX, Me.AreaMeasuringLastEndY), Me.AreaMeasuringmycolor)
            Me.AreaMeasuringLastStartPtX = -1
            Me.AreaMeasuringLastStartPtY = -1
        End If
        For i As Integer = 0 To Me.AreaMeasuringReversibleDrawn.Count - 1 Step 4
            System.Windows.Forms.ControlPaint.DrawReversibleLine(New System.Drawing.Point(Me.AreaMeasuringReversibleDrawn(i), Me.AreaMeasuringReversibleDrawn(i + 1)), New System.Drawing.Point(Me.AreaMeasuringReversibleDrawn(i + 2), Me.AreaMeasuringReversibleDrawn(i + 3)), Me.AreaMeasuringmycolor)
        Next
        Me.AreaMeasuringEraseLast = False
        Me.AreaMeasuringReversibleDrawn.Clear()
        Me.AreaMeasuringlstDrawPoints.Clear()
        frmMain.View.Draw.ClearDrawings()
    End Sub

    Friend Sub AreaMeasuringStop()
        frmMain.MapMain.UDCursorHandle = -1
        frmMain.MapMain.MapCursor = MapWinGIS.tkCursor.crsrArrow
        frmMain.MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
        'frmMain.tbbMeasureArea.Checked = False
        Me.AreaMeasuringCurrently = False
        Me.AreaMeasuringStartPtX = -1
        Me.AreaMeasuringStartPtY = -1
        Me.AreaMeasuringLastEndX = -1
        Me.AreaMeasuringLastEndY = -1
        Me.AreaMeasuringLastStartPtX = -1
        Me.AreaMeasuringLastStartPtY = -1
        AreaMeasuringClearTempLines()
        '7/31/2006 PM
        'GetOrRemovePanel("Area:", True)
        frmMain.GetOrRemovePanel(frmMain.resources.GetString("msgPanelArea.Text"), True)
    End Sub

    Friend Sub AreaMeasuringBegin()
        'If frmMain.tbbMeasure.Checked Then
        '    MeasuringStop()
        'End If

        If MeasureCursor Is Nothing Then
            MeasureCursor = New Cursor(Me.GetType(), "measuring.ico")
        End If

        frmMain.MapMain.UDCursorHandle = MeasureCursor.Handle
        frmMain.MapMain.MapCursor = MapWinGIS.tkCursor.crsrUserDefined
        frmMain.MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
        ' frmMain.tbbMeasureArea.Checked = True
        Me.AreaMeasuringCurrently = True
        Me.AreaMeasuringlstDrawPoints = New ArrayList
        Me.AreaMeasuringReversibleDrawn = New ArrayList
        frmMain.StatusBar.AddPanel(frmMain.resources.GetString("msgPanelArea.Text"), 0, 100, Windows.Forms.StatusBarPanelAutoSize.Contents)
    End Sub

    Friend Function AreaMeasuringCalculate() As String
        '1/23/2009 JK
        'calculate the area of drawn polygon for the 'measure area' tool
        'and return the result including name of units

        Dim tempPoly As New MapWinGIS.Shape
        tempPoly.Create(MapWinGIS.ShpfileType.SHP_POLYGON)
        ' Loop the points, inserting them into new poly
        Dim i As Integer
        For i = 0 To Me.AreaMeasuringlstDrawPoints.Count - 1
            tempPoly.InsertPoint(Me.AreaMeasuringlstDrawPoints(i), tempPoly.numPoints)
        Next
        'Add the first point again to complete the polygon
        tempPoly.InsertPoint(Me.AreaMeasuringlstDrawPoints(0), tempPoly.numPoints)

        Dim DataUnit As UnitOfMeasure    'the unit specified in Project Settings..Map Data Units
        Dim MeasureUnit As UnitOfMeasure 'the unit specified in Project Settings..Show Additional Unit
        DataUnit = MapWinGeoProc.UnitConverter.StringToUOM(modMain.frmMain.Project.MapUnits)
        MeasureUnit = DataUnit
        If modMain.ProjInfo.ShowStatusBarCoords_Alternate.ToLower() <> "(none)" Then
            MeasureUnit = MapWinGeoProc.UnitConverter.StringToUOM(modMain.ProjInfo.ShowStatusBarCoords_Alternate)
        End If

        'Convert the total area from Map Data units to Alternate units
        Dim newArea As Double = MapWinGeoProc.Utils.Area(tempPoly, DataUnit)

        'if Map Data Units are DecimalDegrees, the area() function returns the result in kilometers
        If DataUnit = UnitOfMeasure.DecimalDegrees Then DataUnit = UnitOfMeasure.Kilometers
        If MeasureUnit = UnitOfMeasure.DecimalDegrees Then MeasureUnit = DataUnit

        newArea = MapWinGeoProc.UnitConverter.ConvertArea(DataUnit, MeasureUnit, newArea)

        Dim squared As String = (Convert.ToChar(178)).ToString() 'the exponent sign

        '1/23/2009 JK - internationalization - show area in the in status bar
        Dim msgArea As String = String.Format("{0} {1}{2}", _
        frmMain.formatDistance(newArea), MeasureUnit.ToString, squared)

        Return msgArea
    End Function

    Friend Sub MeasuringBegin()
        'If frmMain.tbbMeasureArea.Checked Then
        '    AreaMeasuringStop()
        'End If

        If MeasureCursor Is Nothing Then
            MeasureCursor = New Cursor(Me.GetType(), "measuring.ico")
        End If

        frmMain.MapMain.UDCursorHandle = MeasureCursor.Handle
        frmMain.MapMain.MapCursor = MapWinGIS.tkCursor.crsrUserDefined
        frmMain.MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
        '  frmMain.tbbMeasure.Checked = True
        Me.MeasuringCurrently = True
        Me.MeasuringDrawing = -1
        Me.MeasuringPreviousSegments = New ArrayList
        ''7/31/2006 PM
        'StatusBar.AddPanel("Distance: Click first point", 0, 100, Windows.Forms.StatusBarPanelAutoSize.Contents)
        frmMain.StatusBar.AddPanel(frmMain.resources.GetString("msgPanelDistance.Text"), 0, 100, Windows.Forms.StatusBarPanelAutoSize.Contents)
    End Sub

    Friend Sub MeasuringStop()
        frmMain.MapMain.UDCursorHandle = -1
        frmMain.MapMain.MapCursor = MapWinGIS.tkCursor.crsrArrow
        frmMain.MapMain.CursorMode = MapWinGIS.tkCursorMode.cmNone
        '  frmMain.tbbMeasure.Checked = False
        Me.MeasuringCurrently = False
        Me.MeasuringTotalDistance = 0
        Me.MeasuringStartX = 0
        Me.MeasuringStartY = 0
        Me.MeasuringScreenPointStart = Nothing
        Me.MeasuringScreenPointFinish = Nothing
        Me.MeasuringPreviousSegments.Clear()
        Me.MeasuringPreviousSegments = Nothing
        If (Not Me.MeasuringDrawing = -1) Then
            frmMain.MapMain.ClearDrawing(Me.MeasuringDrawing)
            Me.MeasuringDrawing = -1
        End If
        '7/31/2006 PM
        'GetOrRemovePanel("Distance:", True)
        frmMain.GetOrRemovePanel(frmMain.resources.GetString("msgPanelDistance.Text"), True)
    End Sub

    Friend Sub MeasuringDrawPreviousSegments()
        For i As Integer = 0 To Me.MeasuringPreviousSegments.Count - 1 Step 4
            frmMain.MapMain.DrawLine(CType(Me.MeasuringPreviousSegments(i), Double), CType(Me.MeasuringPreviousSegments(i + 1), Double), CType(Me.MeasuringPreviousSegments(i + 2), Double), CType(Me.MeasuringPreviousSegments(i + 3), Double), 2, Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)))
        Next
    End Sub
#End Region

    
End Class
