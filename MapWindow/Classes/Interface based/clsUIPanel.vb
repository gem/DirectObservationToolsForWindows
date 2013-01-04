Public Class clsUIPanel
    Implements MapWindow.Interfaces.UIPanel

    Friend m_Panels As New Hashtable
    Friend m_OnCloseHandlers As New Hashtable

    <CLSCompliant(False)> _
    Public Shared Function SimplifyDockstate(ByVal DockStyle As System.Windows.Forms.DockStyle) As WeifenLuo.WinFormsUI.Docking.DockState
        Select Case DockStyle
            Case Windows.Forms.DockStyle.Bottom
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockBottom
            Case Windows.Forms.DockStyle.Fill
                Return WeifenLuo.WinFormsUI.Docking.DockState.Document
            Case Windows.Forms.DockStyle.Left
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
            Case Windows.Forms.DockStyle.None
                Return WeifenLuo.WinFormsUI.Docking.DockState.Float
            Case Windows.Forms.DockStyle.Right
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockRight
            Case Windows.Forms.DockStyle.Top
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockTop
        End Select
    End Function

    <CLSCompliant(False)> _
    Public Shared Function SimplifyDockstate(ByVal DockStyle As MapWindow.Interfaces.MapWindowDockStyle) As WeifenLuo.WinFormsUI.Docking.DockState
        Select Case DockStyle
            Case Interfaces.MapWindowDockStyle.BottomAutoHide
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide
            Case Interfaces.MapWindowDockStyle.LeftAutoHide
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide
            Case Interfaces.MapWindowDockStyle.RightAutoHide
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockRightAutoHide
            Case Interfaces.MapWindowDockStyle.TopAutoHide
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockTopAutoHide
            Case Interfaces.MapWindowDockStyle.Bottom
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockBottom
            Case Interfaces.MapWindowDockStyle.Left
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
            Case Interfaces.MapWindowDockStyle.Right
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockRight
            Case Interfaces.MapWindowDockStyle.Top
                Return WeifenLuo.WinFormsUI.Docking.DockState.DockTop
            Case Interfaces.MapWindowDockStyle.None
                Return WeifenLuo.WinFormsUI.Docking.DockState.Float
        End Select
    End Function

    Public Function CreatePanel(ByVal Caption As String, ByVal DockStyle As System.Windows.Forms.DockStyle) As System.Windows.Forms.Panel Implements Interfaces.UIPanel.CreatePanel
        Static nCount As Integer = 0

        If m_Panels.Contains(Caption) Then Return m_Panels(Caption).controls(0)

        Dim ContentPanel As New Panel
        ContentPanel.Name = "ContentPanel"
        ContentPanel.Dock = Windows.Forms.DockStyle.Fill
        Dim floatPanel As New clsMWDockPanel(Caption)
        floatPanel.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float

        floatPanel.Controls.Add(ContentPanel)
        AddHandler floatPanel.FormClosing, AddressOf MarkClosed
        floatPanel.Show(frmMain.dckPanel)
        ' Start Paul Meems, May 30 2010
        ' Don't use the default Microsoft icon:
        floatPanel.Icon = frmMain.Icon
        ' End Paul Meems, May 30 2010

        If DockStyle = Windows.Forms.DockStyle.None Then
            floatPanel.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Float
        Else
            floatPanel.DockState = SimplifyDockstate(DockStyle)
        End If

        m_Panels.Add(Caption, floatPanel)
        Return ContentPanel
    End Function

    <CLSCompliant(False)> _
    Public Function CreatePanel(ByVal Caption As String, ByVal DockStyle As MapWindow.Interfaces.MapWindowDockStyle) As System.Windows.Forms.Panel Implements Interfaces.UIPanel.CreatePanel
        Static nCount As Integer = 0

        If m_Panels.Contains(Caption) Then Return m_Panels(Caption).controls(0)

        Dim ContentPanel As New Panel
        ContentPanel.Name = "ContentPanel"
        ContentPanel.Dock = Windows.Forms.DockStyle.Fill
        Dim floatPanel As New clsMWDockPanel(Caption)
        floatPanel.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float

        floatPanel.Controls.Add(ContentPanel)
        AddHandler floatPanel.FormClosing, AddressOf MarkClosed
        floatPanel.Show(frmMain.dckPanel)

        If DockStyle = Interfaces.MapWindowDockStyle.None Then
            floatPanel.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Float
        Else
            floatPanel.DockState = SimplifyDockstate(DockStyle)
        End If

        m_Panels.Add(Caption, floatPanel)
        Return ContentPanel
    End Function

    Private Sub MarkClosed(ByVal sender As Object, ByVal e As FormClosingEventArgs)
        If Not sender Is Nothing Then
            If m_Panels.Contains(sender.text) Then m_Panels.Remove(sender.text)

            If Not m_OnCloseHandlers(sender.text) Is Nothing Then
                Dim a As ArrayList = CType(m_OnCloseHandlers(sender.text), ArrayList)
                While a.Count > 0
                    CType(a.Item(0), MapWindow.Interfaces.OnPanelClose).Invoke(sender.text)
                    a.RemoveAt(0)
                End While
            End If
        End If
    End Sub

    <CLSCompliant(False)> _
    Public Sub AddOnCloseHandler(ByVal Caption As String, ByVal OnCloseFunction As MapWindow.Interfaces.OnPanelClose) Implements MapWindow.Interfaces.UIPanel.AddOnCloseHandler
        If m_OnCloseHandlers(Caption) Is Nothing Then m_OnCloseHandlers(Caption) = New ArrayList()

        CType(m_OnCloseHandlers(Caption), ArrayList).Add(OnCloseFunction)
    End Sub

    Public Sub DeletePanel(ByVal Caption As String) Implements Interfaces.UIPanel.DeletePanel
        If m_Panels(Caption) Is Nothing Then
            'Probably already deleted.
            m_Panels.Remove(Caption)
            Return
        End If

        Dim child As WeifenLuo.WinFormsUI.Docking.DockContent = CType(m_Panels(Caption), WeifenLuo.WinFormsUI.Docking.DockContent)

        While child.Controls.Count > 0
            child.Controls.RemoveAt(0)
        End While

        m_Panels.Remove(Caption)
        child.Close()
    End Sub

    Public Sub SetPanelVisible(ByVal Caption As String, ByVal Visible As Boolean) Implements Interfaces.UIPanel.SetPanelVisible
        If Not m_Panels(Caption) Is Nothing Then
            Dim child As WeifenLuo.WinFormsUI.Docking.DockContent = CType(m_Panels(Caption), WeifenLuo.WinFormsUI.Docking.DockContent)
            If Not Visible Then child.Hide()
            If Visible Then child.Show()
        End If
    End Sub
End Class
