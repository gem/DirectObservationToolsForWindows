'********************************************************************************************************
'File Name: PrjSetGrid.vb
'Description: Main GUI interface for the MapWindow application.
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
'10/6/2005 - Initial version created by Lailin Chen
'1/22/2006 - cdm - Added the ResizeBehavior property
'5/11/2007 - Tom Shanley (tws) - added the SaveShapeSettings property
'3/31/2008 - Jiri Kadlec (jk) - added the Language Settings options (OverrideSystemLocale and Locale)
'5/4/2008 - Jiri Kadlec (jk) - moved application-level settings to AppSetGrid, enabled internationalization
'                              The "Category", "Display Name" and "Description" entries are now stored in the 
'                              resource file (PrjSetGrid.resx) enabling translation to other languages
'5/26/2008 - Jiri Kadlec (jk) - added a "Configuration File Name" property. This is the .mwcfg file where the
'                               MapWindow application-level configuration settings are saved.
'25/05/2010 - Dave Gilmore (DG) - Added in pointers to new MapWinGeoProc.Projections class
'********************************************************************************************************
Option Strict Off
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports MapWindow.PropertyGridUtils
Imports System.Drawing.Design
Imports MapWindow.Controls.Projections

'5/6/2008 jk - the following attribute enables localization of the class
<TypeConverter(GetType(MapWindow.PropertyGridUtils.GlobalizedTypeConverter))> _
Public Class PrjSetGrid

    ' The original [Project projection] Tab
    Private m_enableSpecifyProjection As Boolean
    Private m_MainCategory As String
    Private m_SubCategory As String
    Private m_Name As String
    Public Shared m_CurrentMain As String
    Public Shared m_CurrentSub As String

    ' The original [Coordinate Display] Tab
    Private m_MapCoordinates As Boolean
    Private m_LatitudeLongitude As Boolean

    'Private projections As New clsProjections
    '---------------------------------------------------------------------------------

#Region "PrjSetGrid Constructor"

    Public Sub New()
        'If there's a project projection, load it
        'If Not modMain.ProjInfo.ProjectProjection = "" Then
        '    'm_enableSpecifyProjection = True
        '    'Dim p As clsProjections.clsProjection = projections.FindProjectionByPROJ4(modMain.ProjInfo.ProjectProjection)
        '    '' Chris M - test for nothing first, in case of *very* unrecognized unprojection in the above function.
        '    'If Not p Is Nothing Then
        '    '    m_MainCategory = p.MainCateg
        '    '    m_SubCategory = p.Category
        '    '    m_Name = p.Name
        '    '    m_CurrentMain = m_MainCategory
        '    '    m_CurrentSub = m_SubCategory
        '    'Else
        '    '    m_enableSpecifyProjection = False
        '    '    m_MainCategory = ""
        '    '    m_SubCategory = ""
        '    '    m_Name = ""
        '    '    m_CurrentMain = ""
        '    '    m_CurrentSub = ""
        '    'End If
        'Else
        '    m_enableSpecifyProjection = False
        '    m_MainCategory = ""
        '    m_SubCategory = ""
        '    m_Name = ""
        '    m_CurrentMain = ""
        '    m_CurrentSub = ""
        'End If
    End Sub

#End Region

#Region "Property Grid Entries"

    ' DISPLAY OPTIONS -- Use Default Background Color
    ' Set this option to "False" to change the map background color of this 
    ' project and override the application-level background color settings.
    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory"), _
    PropertyOrder(0), ReadOnlyAttribute(False)> _
    Public Property BackgroundColor_UseDefault() As Boolean
        Get
            Return modMain.ProjInfo.UseDefaultBackColor
        End Get
        Set(ByVal Value As Boolean)
            modMain.ProjInfo.UseDefaultBackColor = Value '5/5/2008 JK

            'frmMain.MapMain.CtlBackColor = Value
            'Bug 767 - for some reason, this is necessary in order to pick up
            'system colors. Makes sense somewhat given how the object gets translated
            'to the OCX later on.
            Dim backColor As Color = AppInfo.DefaultBackColor
            If Value = True Then
                backColor = AppInfo.DefaultBackColor
                frmMain.View.BackColor = Color.FromArgb(backColor.A, backColor.R, backColor.G, backColor.B)
                frmMain.SetModified(True)
            Else
                backColor = modMain.ProjInfo.ProjectBackColor
                frmMain.View.BackColor = Color.FromArgb(backColor.A, backColor.R, backColor.G, backColor.B)
                frmMain.SetModified(True)
            End If
        End Set
    End Property


    ' DISPLAY OPTIONS -- Map Background Color
    ' Background color of main map (project-level). This setting overrides
    ' the default application-level map background color.

    <GlobalizedProperty(CategoryId:="DisplayOptionsCategory"), _
    PropertyOrder(1), ReadOnlyAttribute(False)> _
    Public Property BackgroundColor() As System.Drawing.Color
        Get
            Return modMain.ProjInfo.ProjectBackColor
        End Get
        Set(ByVal Value As System.Drawing.Color)

            modMain.ProjInfo.ProjectBackColor = Value '5/5/2008 JK

            If modMain.ProjInfo.UseDefaultBackColor = False Then
                'frmMain.MapMain.CtlBackColor = Value
                'Bug 767 - for some reason, this is necessary in order to pick up
                'system colors. Makes sense somewhat given how the object gets translated
                'to the OCX later on.
                frmMain.View.BackColor = Color.FromArgb(Value.A, Value.R, Value.G, Value.B)
                frmMain.SetModified(True)
            End If
        End Set
    End Property



    ' COORDINATE DISPLAY -- Show Map Data Units
    ' Sets whether the coordinates should be displayed in the status bar in the map data units.
    <GlobalizedProperty(CategoryId:="CoordinateDisplayCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ShowDataUnits() As Boolean
        Get
            Return modMain.ProjInfo.ShowStatusBarCoords_Projected
        End Get
        Set(ByVal Value As Boolean)
            modMain.ProjInfo.ShowStatusBarCoords_Projected = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' COORDINATE DISPLAY -- Show Additional Unit
    ' Indicates whether additional units should be shown in addition to the map data units.
    <GlobalizedProperty(CategoryId:="CoordinateDisplayCategory"), _
    TypeConverter(GetType(MapUnitCls)), _
    ReadOnlyAttribute(False)> _
    Public Property ShowAdditionalUnits() As String
        Get
            'If it's not set to anything and the map units are not lat/long, default to lat/long.
            If modMain.ProjInfo.ShowStatusBarCoords_Alternate = "(None)" And Not modMain.ProjInfo.m_MapUnits = "" And Not modMain.ProjInfo.m_MapUnits = "Lat/Long" Then
                modMain.ProjInfo.ShowStatusBarCoords_Alternate = "Lat/Long"
            End If
            Return modMain.ProjInfo.ShowStatusBarCoords_Alternate
        End Get
        Set(ByVal Value As String)
            modMain.ProjInfo.ShowStatusBarCoords_Alternate = Value
        End Set
    End Property

    ' COORDINATE DISPLAY -- Status Bar Decimals (Standard)
    ' Number of digits to round on standard coordinates.
    <GlobalizedProperty(CategoryId:="CoordinateDisplayCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property StatusCoordsRounding() As Integer
        Get
            Return modMain.ProjInfo.StatusBarCoordsNumDecimals
        End Get
        Set(ByVal value As Integer)
            modMain.ProjInfo.StatusBarCoordsNumDecimals = value
        End Set
    End Property

    ' COORDINATE DISPLAY -- Status Bar Decimals (Additional)
    ' Number of digits to round on alternate coordinates.
    <GlobalizedProperty(CategoryId:="CoordinateDisplayCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property StatusAlternateCoordsRounding() As Integer
        Get
            Return modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals
        End Get
        Set(ByVal value As Integer)
            modMain.ProjInfo.StatusBarAlternateCoordsNumDecimals = value
        End Set
    End Property

    ' COORDINATE DISPLAY -- Status Bar Comma Separators (Standard)
    ' Display commas in coordinates?
    <GlobalizedProperty(CategoryId:="CoordinateDisplayCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property StatusCoordsCommas() As Boolean
        Get
            Return modMain.ProjInfo.StatusBarCoordsUseCommas
        End Get
        Set(ByVal value As Boolean)
            modMain.ProjInfo.StatusBarCoordsUseCommas = value
        End Set
    End Property

    ' COORDINATE DISPLAY -- Status Bar Comma Separators (Additional)
    ' Display commas in coordinates?
    <GlobalizedProperty(CategoryId:="CoordinateDisplayCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property StatusAlternateCoordsCommas() As Boolean
        Get
            Return modMain.ProjInfo.StatusBarAlternateCoordsUseCommas
        End Get
        Set(ByVal value As Boolean)
            modMain.ProjInfo.StatusBarAlternateCoordsUseCommas = value
        End Set
    End Property

    ' COORDINATE DISPLAY -- Map Data Units
    ' Sets the unit that the map data is assumed to be in.
    <GlobalizedProperty(CategoryId:="CoordinateDisplayCategory"), _
    TypeConverter(GetType(MapUnitCls)), _
    ReadOnlyAttribute(False)> _
    Public Property DataUnits() As String
        Get
            If (modMain.frmMain.Project.MapUnits = "") Then
                Return "(None)"
            Else
                Return modMain.frmMain.Project.MapUnits
            End If
        End Get
        Set(ByVal Value As String)
            If (Value = "(None)") Then
                modMain.frmMain.Project.MapUnits = ""
            Else
                modMain.frmMain.Project.MapUnits = Value
            End If
            frmMain.SetModified(True)
        End Set
    End Property

    <GlobalizedProperty(CategoryId:="ProjectBehaviorCategory"), _
    ReadOnlyAttribute(True)> _
    Public Property MwConfigFileName() As String
        Get
            Return modMain.ProjInfo.ConfigFileName
        End Get
        Set(ByVal value As String)
            modMain.ProjInfo.ConfigFileName = value
        End Set
    End Property

#End Region

#Region "Update Projection Selection"

    'Private Sub UpdateProjectionSelection()
    '    If m_enableSpecifyProjection = True And ("" = m_Name Or "" = m_MainCategory Or "" = m_SubCategory) Then
    '        ' Can't do anything now
    '        Return
    '    Else
    '        If m_enableSpecifyProjection And Name = "Custom Projection" Then
    '            'Do nothing here either -- wait for the dialog
    '            'to be presented elsewhere.
    '        ElseIf m_enableSpecifyProjection Then
    '            'DG TODO: Add method to Projections.cs
    '            'ProjInfo.ProjectProjection = projections.FindProjectionByCatAndName(m_MainCategory, m_SubCategory, m_Name)
    '        End If

    '        frmMain.SetModified(True)
    '    End If
    'End Sub

#End Region

#Region "Projection Classes"

    ''Projection main category (used for selecting projections)
    'Public Class MainCategoryCls
    '    Inherits StringConverter
    '    Private projections As New clsProjections
    '    'DG Added:
    '    Private newProjections As New MapWinGeoProc.Projections.Projections

    '    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
    '        Return True
    '    End Function

    '    Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
    '        Dim list As New ArrayList
    '        'For i As Integer = 0 To projections.ProjectionList.Count - 1
    '        '    If Not list.Contains(projections.ProjectionList.Item(i).MainCateg) Then
    '        '        list.Add(projections.ProjectionList.Item(i).MainCateg)
    '        '    End If
    '        'Next

    '        'DG Added:
    '        'TODO: Get these values from MapWinGeoProc.Projections.Projections
    '        list.Add("Geographic Coordinate Systems")
    '        list.Add("Projected Coordinate Systems")
    '        list.Sort()

    '        list.Add("Custom Projection")
    '        Return New StandardValuesCollection(list)

    '    End Function

    '    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
    '        Return False
    '    End Function

    'End Class

    ''Projection subcategory (used for selecting projections)
    'Public Class SubCategoryCls
    '    Inherits StringConverter
    '    Private projections As New clsProjections
    '    'DG Added:
    '    Private newProjections As New MapWinGeoProc.Projections.Projections
    '    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
    '        Return True
    '    End Function

    '    Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection

    '        'Dim list As New ArrayList
    '        'For i As Integer = 0 To projections.ProjectionList.Count - 1
    '        '    If projections.ProjectionList.Item(i).MainCateg.ToLower() = PrjSetGrid.m_CurrentMain.ToLower() Then
    '        '        If Not list.Contains(projections.ProjectionList.Item(i).Category) Then
    '        '            list.Add(projections.ProjectionList.Item(i).Category)
    '        '        End If
    '        '    End If
    '        'Next

    '        'list.Sort()

    '        'Return New StandardValuesCollection(list)

    '        'DG Added:
    '        Dim isGeographicProjections As Boolean

    '        If PrjSetGrid.m_CurrentMain.Contains("Projected") Then
    '            isGeographicProjections = False
    '        Else
    '            isGeographicProjections = True
    '        End If

    '        Return New StandardValuesCollection(newProjections.GetMajorCategories(isGeographicProjections))

    '    End Function

    '    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
    '        Return False
    '    End Function

    'End Class

    ''Projection Name (used for selecting projections)
    'Public Class NameCls
    '    Inherits StringConverter
    '    Private projections As New clsProjections
    '    'DG Added:
    '    Private newProjections As New MapWinGeoProc.Projections.Projections

    '    'support combo box style select
    '    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
    '        Return True
    '    End Function

    '    'generate the list for selection
    '    Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
    '        'Dim list As New ArrayList
    '        'For i As Integer = 0 To projections.ProjectionList.Count - 1
    '        '    If projections.ProjectionList.Item(i).MainCateg.ToLower() = PrjSetGrid.m_CurrentMain.ToLower() And projections.ProjectionList.Item(i).Category.ToLower() = m_CurrentSub.ToLower() Then
    '        '        If Not list.Contains(projections.ProjectionList.Item(i).Name) Then
    '        '            list.Add(projections.ProjectionList.Item(i).Name)
    '        '        End If
    '        '    End If
    '        'Next

    '        'list.Sort()

    '        'DG Added:
    '        Dim isGeographicProjections As Boolean

    '        If PrjSetGrid.m_CurrentMain.Contains("Projected") Then
    '            isGeographicProjections = False
    '        Else
    '            isGeographicProjections = True
    '        End If

    '        Return New StandardValuesCollection(newProjections.GetMinorCategories(PrjSetGrid.m_CurrentSub.ToString(), isGeographicProjections))

    '    End Function

    '    'do not need the values exclusive to each other
    '    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
    '        Return False
    '    End Function

    'End Class

#End Region

#Region "Map Unit Class"

    'List of Map Data Units
    Public Class MapUnitCls
        Inherits StringConverter

        'support combo box style select
        Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        'generate the list for selection
        Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim list As New ArrayList

            list.Add("Lat/Long")
            list.Add("Meters")
            list.Add("Centimeters")
            list.Add("Feet")
            list.Add("Inches")
            list.Add("Kilometers")
            list.Add("Miles")
            list.Add("Millimeters")
            list.Add("Yards")
            list.Add("NauticalMiles") '08/28/2008

            Return New StandardValuesCollection(list)

        End Function

        'do not need the values exclusive to each other
        Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
            Return False
        End Function
    End Class
#End Region

#Region "Projections"
    ' PROJECT PROJECTION -- Show Mismatch Warnings?
    ' Sets whether to prompt when a projection mismatch is detected between datasets.
    <GlobalizedProperty(CategoryId:="ProjectProjectionCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ShowProjectionMismatchWarnings() As Boolean
        Get
            Return Not AppInfo.NeverShowProjectionDialog
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.NeverShowProjectionDialog = Not Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' PROJECT PROJECTION -- Mismatch behavior
    <GlobalizedProperty(CategoryId:="ProjectProjectionCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ProjectionMismatchBehavior() As ProjectionMismatchBehavior
        Get
            Return AppInfo.ProjectionMismatchBehavior
        End Get
        Set(ByVal Value As ProjectionMismatchBehavior)
            AppInfo.ProjectionMismatchBehavior = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' PROJECT PROJECTION -- Absence behavior
    <GlobalizedProperty(CategoryId:="ProjectProjectionCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ProjectionAbsenceBehavior() As ProjectionAbsenceBehavior
        Get
            Return AppInfo.ProjectionAbsenceBehavior
        End Get
        Set(ByVal Value As ProjectionAbsenceBehavior)
            AppInfo.ProjectionAbsenceBehavior = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' PROJECT PROJECTION -- Show report
    <GlobalizedProperty(CategoryId:="ProjectProjectionCategory"), _
    ReadOnlyAttribute(False)> _
    Public Property ShowLoadingReport() As Boolean
        Get
            Return AppInfo.ShowLoadingReport
        End Get
        Set(ByVal Value As Boolean)
            AppInfo.ShowLoadingReport = Value
            frmMain.SetModified(True)
        End Set
    End Property

    ' PROJECT PROJECTION -- Projection?
    <GlobalizedProperty(CategoryId:="ProjectProjectionCategory"), ReadOnlyAttribute(False), _
    TypeConverter(GetType(ProjectionTypeConverter)), Editor(GetType(ProjectionEditor), GetType(UITypeEditor))> _
    Public Property ProjectProjection() As MapWinGIS.GeoProjection
        Get
            Return ProjInfo.GeoProjection
        End Get
        Set(ByVal Value As MapWinGIS.GeoProjection)
            If Not Value Is Nothing Then
                modMain.frmMain.Project.GeoProjection = Value
                frmMain.SetModified(True)
            End If
        End Set
    End Property

    ''' <summary>
    ''' An editor for projection property
    ''' </summary>
    Friend Class ProjectionEditor
        Inherits UITypeEditor

        Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
            Dim proj As MapWinGIS.GeoProjection = modMain.frmMain.m_Project.SetProjectProjectionByDialog()
            Return proj
        End Function

        Public Overloads Overrides Function GetEditStyle(ByVal Context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
            Return UITypeEditorEditStyle.Modal
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

    ''' <summary>
    ''' A converter for projection property
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class ProjectionTypeConverter
        Inherits StringConverter

        Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
            If destinationType Is GetType(String) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
            If value Is Nothing Then
                Return "Not defined"
            Else
                If CType(value, MapWinGIS.GeoProjection).IsEmpty Then
                    Return "Not defined"
                Else
                    Return CType(value, MapWinGIS.GeoProjection).Name
                End If
            End If
        End Function

        Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
            If sourceType Is GetType(MapWinGIS.GeoProjection) Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
#End Region

End Class
