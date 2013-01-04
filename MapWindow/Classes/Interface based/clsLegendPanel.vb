Public Class LegendPanel
    Implements Interfaces.LegendPanel

    Public Sub Close() Implements Interfaces.LegendPanel.Close
        If Not frmMain.legendPanel Is Nothing Then
            frmMain.legendPanel.Close()
            frmMain.legendPanel.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Dock the legend panel
    ''' </summary>
    ''' <param name="dockStyle">The MapWindow dock style</param>
    ''' <remarks></remarks>
    Public Sub DockTo(ByVal dockStyle As Interfaces.MapWindowDockStyle) Implements Interfaces.LegendPanel.DockTo
        If Not frmMain.legendPanel Is Nothing Then
            ' Paul Meems May 2011, show panel if it is hidden:
            frmMain.legendPanel.Show()
            frmMain.legendPanel.Visible = True

            frmMain.legendPanel.DockState = clsUIPanel.SimplifyDockstate(dockStyle)
        End If
    End Sub
End Class
