Public Class PreviewMap
    Implements Interfaces.PreviewMap

    Friend m_ShowLocatorBox As Boolean
    Friend g_ExtentsRect As Rectangle
    Friend g_Dragging As Boolean
    Private m_DrawHandle As Integer = -1

    Public Sub New()
        MyBase.New()
        m_Color = RGB(255, 0, 0)
    End Sub

    '-------------------Private members for public properties-------------------  
    Private m_Color As Integer

    '--------------------------------------PreviewMap Public Interface--------------------------------------

    '-------------------Properties-------------------
    Public Property BackColor() As System.Drawing.Color Implements MapWindow.Interfaces.PreviewMap.BackColor
        Get
            BackColor = frmMain.MapPreview.BackColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            frmMain.MapPreview.BackColor = Value
        End Set
    End Property

    Public Property Picture() As Image Implements Interfaces.PreviewMap.Picture
        Get
            Try
                If frmMain.MapPreview.NumLayers > 0 Then
                    Dim cvter As New MapWinUtility.ImageUtils
                    Return cvter.IPictureDispToImage(CType(frmMain.MapPreview.get_GetObject(frmMain.MapPreview.get_LayerHandle(0)), MapWinGIS.Image).Picture)
                End If
            Catch ex As System.Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
            Return Nothing
        End Get
        Set(ByVal Value As Image)
            Try
                If Not Value Is Nothing Then
                    m_ShowLocatorBox = False
                    Dim img As New MapWinGIS.Image
                    Dim cvter As New MapWinUtility.ImageUtils
                    img.Picture = CType(cvter.ImageToIPictureDisp(Value), stdole.IPictureDisp)
                    frmMain.MapPreview.RemoveAllLayers()
                    frmMain.MapPreview.AddLayer(img, True)
                    frmMain.MapPreview.ZoomToMaxExtents()
                End If
            Catch ex As System.Exception
                ShowError(ex)
            End Try
        End Set
    End Property

    Public Property LocatorBoxColor() As System.Drawing.Color Implements Interfaces.PreviewMap.LocatorBoxColor
        Get
            LocatorBoxColor = MapWinUtility.Colors.IntegerToColor(m_Color)
        End Get
        Set(ByVal Value As System.Drawing.Color)
            m_Color = MapWinUtility.Colors.ColorToInteger(Value)
        End Set
    End Property

    ''' <summary>
    ''' Close the preview panel
    ''' </summary>
    ''' <remarks>Added by Paul Meems, September 2009</remarks>
    Public Sub Close() Implements MapWindow.Interfaces.PreviewMap.Close
        If Not frmMain.previewPanel Is Nothing Then
            ' Paul Meems May 2011, changed to hide instead of close:
            frmMain.previewPanel.Hide()
        End If
    End Sub
    ''' <summary>
    ''' Dock the preview panel
    ''' </summary>
    ''' <param name="dockStyle">The MapWindow dock style</param>
    ''' <remarks>Added by Paul Meems, September 2009</remarks>
    Public Sub DockTo(ByVal dockStyle As MapWindow.Interfaces.MapWindowDockStyle) Implements MapWindow.Interfaces.PreviewMap.DockTo
        If Not frmMain.previewPanel Is Nothing Then
            ' Paul Meems May 2011, show panel if it is hidden:
            frmMain.previewPanel.Show()

            frmMain.previewPanel.DockState = clsUIPanel.SimplifyDockstate(dockStyle)
        End If
    End Sub

    'Chris M August 2006
    Public Sub UpdatePreview() Implements MapWindow.Interfaces.PreviewMap.Update
        GetPictureFromMap(False)
    End Sub

    'Chris M August 2006
    <CLSCompliant(False)> _
    Public Sub UpdatePreview(ByVal UpdateExtents As MapWindow.Interfaces.ePreviewUpdateExtents) Implements MapWindow.Interfaces.PreviewMap.Update
        GetPictureFromMap(IIf(UpdateExtents = Interfaces.ePreviewUpdateExtents.FullExtents, True, False))
    End Sub

    Public Sub GetPictureFromMap() Implements Interfaces.PreviewMap.GetPictureFromMap
        GetPictureFromMap(False)
    End Sub

    ''' <summary>
    ''' Updates preview map with the specified extents
    ''' </summary>
    ''' <param name="FullExtents">If set to true ful extents will be used and current otherwise</param>
    Public Sub GetPictureFromMap(ByVal FullExtents As Boolean)
        Try
            Dim exts As MapWinGIS.Extents
            Dim img As New MapWinGIS.Image()
            Dim ratio As Double
            Dim oldExts As MapWinGIS.Extents = CType(frmMain.MapMain.Extents, MapWinGIS.Extents)

            frmMain.MapPreview.LockWindow(MapWinGIS.tkLockMode.lmLock)
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmLock)

            If FullExtents Then
                exts = frmMain.View.MaxVisibleExtents
                If frmMain.MapPreview.Width < frmMain.MapPreview.Height Then
                    ratio = frmMain.MapPreview.Width / frmMain.MapMain.Width
                Else
                    ratio = frmMain.MapPreview.Height / frmMain.MapMain.Height
                End If
                ratio *= 1.5

                ' when you set the extents they get adjusted to fit the screen.
                frmMain.MapMain.Extents = exts
                exts = CType(frmMain.MapMain.Extents, MapWinGIS.Extents)
                If FullExtents Then frmMain.MapMain.Extents = oldExts
            Else
                exts = CType(frmMain.MapMain.Extents, MapWinGIS.Extents)
            End If

            img = CType(frmMain.MapMain.SnapShot(exts), MapWinGIS.Image)
            'If FullExtents Then frmMain.MapMain.Extents = oldExts
            Dim cvter As New MapWinUtility.ImageUtils
            Dim tmpImg As System.Drawing.Image = MapWinUtility.ImageUtils.ObjectToImage(img.Picture, CInt(img.Width * ratio), CInt(img.Height * ratio))
            img.Picture = CType(cvter.ImageToIPictureDisp(tmpImg), stdole.IPictureDisp)
            img.dX = (exts.xMax - exts.xMin) / img.Width
            img.dY = (exts.yMax - exts.yMin) / img.Height
            img.XllCenter = exts.xMin + 0.5 * img.dX
            img.YllCenter = exts.yMin + 0.5 * img.dX
            img.DownsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBicubic
            img.UpsamplingMode = MapWinGIS.tkInterpolationMode.imHighQualityBicubic

            frmMain.MapPreview.RemoveAllLayers()
            frmMain.MapPreview.AddLayer(img, True)
            frmMain.MapPreview.ExtentPad = 0
            frmMain.MapPreview.ZoomToMaxExtents()
            frmMain.m_PreviewMap.m_ShowLocatorBox = True
            frmMain.m_PreviewMap.UpdateLocatorBox()

            frmMain.mnuZoomPreviewMap.Enabled = (frmMain.MapMain.NumLayers > 0 And frmMain.PreviewMapExtentsValid())
            If Not frmMain.m_Menu.Item("mnuZoomToPreviewExtents") Is Nothing Then frmMain.m_Menu.Item("mnuZoomToPreviewExtents").Enabled = (frmMain.MapMain.NumLayers > 0 And frmMain.PreviewMapExtentsValid())


        Catch ex As Exception
            g_error = ex.Message
            ShowError(ex)
        Finally
            frmMain.MapPreview.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
            frmMain.MapMain.LockWindow(MapWinGIS.tkLockMode.lmUnlock)
        End Try
    End Sub

    Public Function GetPictureFromFile(ByVal Filename As String) As Boolean Implements Interfaces.PreviewMap.GetPictureFromFile
        Dim img As New MapWinGIS.Image()
        If InStr(1, LCase(img.CdlgFilter), LCase(MapWinUtility.MiscUtils.GetExtensionName(Filename)), vbTextCompare) <> 0 Then
            If img.Open(Filename, MapWinGIS.ImageType.USE_FILE_EXTENSION, False) = False Then
                g_error = "Failed to open file to load into preview map"
                Return False
            Else ' the file opened fine
                Dim tStr As String = Dir(Filename.TrimEnd(CType(MapWinUtility.MiscUtils.GetExtensionName(Filename), Char())) & ".*")
                If tStr <> "" Then
                    Select Case LCase(MapWinUtility.MiscUtils.GetExtensionName(tStr))
                        Case "bpw", "gfw" ' a world file exists
                            m_ShowLocatorBox = True
                        Case Else ' No world file available.  Disable locator box
                            m_ShowLocatorBox = False
                    End Select
                End If
                frmMain.MapPreview.AddLayer(img, True)
            End If
        Else
            g_error = "Unsupported picture format"
            Return False
        End If
        Return True
    End Function

    Friend Sub UpdateLocatorBox()
        frmMain.MapPreview.ZoomToMaxExtents()

        Dim exts As MapWinGIS.Extents = CType(frmMain.MapMain.Extents, MapWinGIS.Extents)
        Dim newLeft As Double, newRight As Double, newTop As Double, newBottom As Double

        If m_ShowLocatorBox = False Then
            frmMain.MapPreview.ClearDrawings()
            Exit Sub
        End If

        ' Get the pixel bounds of the box
        frmMain.MapPreview.ProjToPixel(exts.xMin, exts.yMax, newLeft, newTop)
        frmMain.MapPreview.ProjToPixel(exts.xMax, exts.yMin, newRight, newBottom)

        Try
            g_ExtentsRect = New Rectangle(CInt(newLeft), CInt(newTop), CInt(newRight - newLeft), CInt(newBottom - newTop))

            DrawBox(g_ExtentsRect)
        Catch
            ' probably an overflow and underflow.  Ignore this error
        End Try
    End Sub

    Friend Sub DrawBox(ByVal rect As Rectangle)
        'Create a new drawing
        With frmMain.MapPreview
            Dim color As UInt32 = System.Convert.ToUInt32(m_Color)
            If m_DrawHandle >= 0 Then
                .ClearDrawing(m_DrawHandle)
            End If
            m_DrawHandle = .NewDrawing(MapWinGIS.tkDrawReferenceList.dlScreenReferencedList)

            .DrawLine(rect.Left, rect.Top, rect.Right, rect.Top, 2, color)
            .DrawLine(rect.Right, rect.Top, rect.Right, rect.Bottom, 2, color)
            .DrawLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom, 2, color)
            .DrawLine(rect.Left, rect.Bottom, rect.Left, rect.Top, 2, color)
        End With
    End Sub

End Class


