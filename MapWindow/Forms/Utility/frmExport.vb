' Map Image Export Options Dialog
'
' 04/25/2007 | t shanley | original file
'
' if you don't pick a layer you will get the same results as before: an image file of just what you see
' or you can pick a layer to clip to - controls the extent exported - then set zoom or width, depending on type
'
' why "zoom" for images but "width" for shapes?
'   images have an original size.  most likely you either want it just like that, or some exact 
'   multiple so that the resampling is optimal. so zoom seems best for these.
'
'   shapefile don't have an original size, so that simple zoom factor doesn't work. thus we ask, 
'   "exactly how wide did you want that?" and use the aspect ration to get the height that matches
'
' why only shapefile or image?
'   image is useful if you are updating a map by overlay of shapes or rasters
'   otherwise, you can always create a non-display layer shapefile and set the extents you want (add a shape)
'   so, these two are *sufficient*. 

Imports System.Windows.Forms

Public Class frmExport

#Region "properties"
    Public Shared MAX_SUGGESTED_WIDTH As Long = 20000
    Public Shared BIG_JPEG As Long = 4000

    Private frmMain As MapWindowForm = Nothing
    Friend sfd As SaveFileDialog = Nothing
    Friend newfilename As String = Nothing

    Private layerAR As Double = 1.0
    Private layerOriginalWidth As Integer = 0
    Private layerOriginalHeight As Integer = 0
    Private isImage As Boolean

    Friend Property MainForm() As MapWindowForm
        Get
            Return frmMain
        End Get
        Set(ByVal value As MapWindowForm)
            frmMain = value
            init()
        End Set
    End Property

    Public ReadOnly Property SelectedLayer() As Integer
        Get
            Dim rv As Integer = -1
            Try
                rv = CType(Me.cbClipToLayer.SelectedItem, NameVal).Value
            Catch ex As Exception
            End Try
            Return rv
        End Get
    End Property

    Public ReadOnly Property ImageWidth() As Integer
        Get
            Return IIf(Me.isImage, 0, Me.workingWidth)
        End Get
    End Property

    Public ReadOnly Property ImageZoom() As Double
        Get
            Return IIf(Me.isImage, Double.Parse(Me.txtOutFileWidth.Text), 0.0)
        End Get
    End Property


#End Region

#Region "internal functions"
    Private Function workingWidth() As Long
        Dim rv As Long = 0
        Try
            Dim factorW As Double = Double.Parse(Me.txtOutFileWidth.Text)
            If isImage Then
                rv = Math.Truncate(factorW * layerOriginalWidth)
            Else
                rv = factorW
            End If
        Catch ex As Exception
        End Try
        Return rv
    End Function

    Private Function workingHeight() As Long
        Dim rv As Long = 0
        Try
            Dim factorW As Double = Double.Parse(Me.txtOutFileWidth.Text)
            If isImage Then
                rv = Math.Truncate(factorW * layerOriginalHeight)
            Else
                rv = factorW / layerAR
            End If
        Catch ex As Exception
        End Try
        Return rv
    End Function

    ' this is only an estimate!  the export may still succeed if this chgeck fails, 
    ' and it may still fail even if this check passes.
    ' if you are pushing at your memory limits, you may sometimes have just one layer 
    ' fail silenly, and get an image file without that layer - seen with raster layers.
    Private Function checkAvailableMemory() As Boolean
        Dim rv As Boolean
        Try
            Dim availMB As Integer = (pcCommitLimit.NextValue - pcCommittedBytes.NextValue) / (1024 ^ 2)
            Dim neededMB As Integer = 0
            Dim factorW As Double = Double.Parse(Me.txtOutFileWidth.Text)
            If isImage Then
                ' the first '3' is for 24bpp; the second '3' is for observed redundant copies during processing
                neededMB = (factorW * (layerOriginalWidth * (layerOriginalWidth / layerAR)) * 3 * 3) / (1024 ^ 2)
            Else
                neededMB = ((factorW * (factorW / layerAR)) * 3 * 3) / (1024 ^ 2)
            End If
            If availMB < neededMB Then
                Dim msg As String = "Available memory = " & availMB & "MB" & vbCrLf
                msg &= "Estimated memory needed = " & neededMB & "MB" & vbCrLf
                msg &= "Proceed anyway?"
                Dim dr As DialogResult = mapwinutility.logger.msg(msg, MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Export May Fail")
                rv = (dr = Windows.Forms.DialogResult.Yes)
            Else
                rv = True
            End If
        Catch ex As Exception
            ' don't let problems here - e.g. missing performance counters - stop the process
            rv = True
        End Try
        Return rv
    End Function

    Private Sub init()
        Dim alLayers As New ArrayList
        alLayers.Add(New NameVal("", -1))
        For ix As Integer = 0 To frmMain.MapMain.NumLayers - 1
            Dim l As Layer = frmMain.Layers(frmMain.MapMain.get_LayerHandle(ix))
            If l.LayerType = Interfaces.eLayerType.Image Or l.LayerType = Interfaces.eLayerType.LineShapefile _
            Or l.LayerType = Interfaces.eLayerType.PointShapefile Or l.LayerType = Interfaces.eLayerType.PolygonShapefile Then
                alLayers.Add(New NameVal(l.Name, ix))
            End If
        Next
        Me.cbClipToLayer.DataSource = alLayers
        showSize()
    End Sub

    Private Sub showSize()
        Dim wid, hite As Integer
        Try
            If Me.SelectedLayer > -1 Then
                wid = Me.workingWidth
                hite = Me.workingHeight
            Else
                With Me.frmMain.mapPanel
                    wid = .Width
                    hite = .Height
                End With
            End If
        Catch ex As Exception
        Finally
            Me.lblSize.Text = wid & " x " & hite
        End Try
    End Sub

#End Region

#Region "form event handlers"
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.newfilename = Me.txtExportFile.Text
        If Not Me.newfilename > "" Then
            mapwinutility.logger.msg("Output file is required", MsgBoxStyle.Critical)
            Return
        End If
        If Me.txtOutFileWidth.Visible Then
            Try
                Dim tester As Double = Double.Parse(Me.txtOutFileWidth.Text)
            Catch ex As Exception
                mapwinutility.logger.msg("Invalid " & IIf(Me.lblWidth.Visible, "width", "zoom"), MsgBoxStyle.Critical)
                Return
            End Try
            If Not Me.checkAvailableMemory Then
                Return
            End If
            If Me.newfilename.ToLower.EndsWith("jpg") Then
                If Me.workingWidth > BIG_JPEG Then
                    Dim msg As String = "Export of large JPG files can be very slow; proceed anyway?"
                    Dim dr As DialogResult = MapWinUtility.Logger.Msg(msg, MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Export May Be Slow")
                    If Not (dr = Windows.Forms.DialogResult.Yes) Then Return
                End If
            End If
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cbClipToLayer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbClipToLayer.SelectedIndexChanged
        ' here we enable/disable/label the width/zoom input
        ' determine the initial suggested width
        ' and keep track of the aspect ratio, so we can estimate memory needed, later
        Try
            isImage = False
            Dim lix As Integer = SelectedLayer() 'CType(Me.cbClipToLayer.SelectedItem, NameVal).Value
            If lix >= 0 Then
                Dim l As Layer = frmMain.Layers(frmMain.MapMain.get_LayerHandle(lix))
                Dim cx, cy As Double
                With l.Extents
                    cx = .xMax - .xMin
                    cy = .yMax - .yMin
                End With
                Me.layerAR = cx / cy
                If l.LayerType = Interfaces.eLayerType.Image Then
                    isImage = True
                    lblZoom.Visible = True
                    lblWidth.Visible = False
                    Dim img As MapWinGIS.Image = CType(l.GetObject(), MapWinGIS.Image)
                    layerOriginalWidth = img.OriginalWidth
                    layerOriginalHeight = img.OriginalHeight
                    ' make a suggestion for the zoom
                    Me.txtOutFileWidth.Text = "1.0"
                Else
                    lblZoom.Visible = False
                    lblWidth.Visible = True
                    ' make a suggestion for the width, based on the x-extent and the projection
                    Dim sf As MapWinGIS.Shapefile = CType(l.GetObject(), MapWinGIS.Shapefile)
                    Dim proj As String = sf.Projection
                    Dim suggestWidth As Long
                    If proj Is Nothing OrElse proj.IndexOf("units=m") > -1 Then
                        suggestWidth = cx ' 1 mpp
                    Else
                        suggestWidth = cx * 10000 ' 10k ppd
                    End If
                    If suggestWidth > MAX_SUGGESTED_WIDTH Then
                        suggestWidth /= (Math.Truncate(suggestWidth / MAX_SUGGESTED_WIDTH) + 1)
                        suggestWidth -= suggestWidth Mod 10
                    End If
                    Me.txtOutFileWidth.Text = suggestWidth
                End If
                Me.txtOutFileWidth.Enabled = True
                Me.txtOutFileWidth.Visible = True
            Else
                Me.txtOutFileWidth.Visible = False
                Me.txtOutFileWidth.Enabled = False
                lblZoom.Visible = False
                lblWidth.Visible = False
            End If
        Catch ex As Exception
            MapWinUtility.Logger.Msg(ex.Message & vbCrLf & ex.StackTrace)
        Finally
            showSize()
        End Try
    End Sub

    Private Sub pbSaveExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbSaveExport.Click
        If sfd.ShowDialog() = DialogResult.OK Then
            newfilename = sfd.FileName
            Me.txtExportFile.Text = newfilename
        Else
            Exit Sub
        End If
    End Sub

    Private Sub txtOutFileWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOutFileWidth.TextChanged
        showSize()
    End Sub

#End Region

#Region "inner classes"
    ' local convenience class to fill the combo box with names and carry the layer index for use later
    Class NameVal
        Public Name As String
        Public Value As Integer
        Public Sub New(ByVal pName As String, ByVal pVal As Integer)
            Name = pName
            Value = pVal
        End Sub
        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class
#End Region

    Private Sub frmExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class

