Imports System.Windows.Forms.Design

Friend Class LineWidthControl
    Inherits System.Windows.Forms.UserControl

    Private Const c_ItemHeight As Integer = 20
    Private Const c_ItemSpacing As Integer = 2
    Private Const c_MaxLineWidth As Integer = 9
    Private m_CurrentSelection As Integer = -1
    Private m_retval As Integer = -1
    Private m_Provider As IWindowsFormsEditorService

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'LineWidthControl
        '
        Me.Name = "LineWidthControl"
        Me.Size = New System.Drawing.Size(120, 222)

    End Sub

#End Region

    Private Sub LineWidthControl_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim rect As System.Drawing.Rectangle = e.ClipRectangle
        Dim i As Integer, curY As Integer = c_ItemSpacing

        For i = 0 To c_MaxLineWidth
            If i <> 0 Then
                g.DrawLine(New Pen(System.Drawing.Color.Black, i), 2 * c_ItemSpacing, curY + (c_ItemHeight \ 2), Me.Width - (3 * c_ItemSpacing), curY + (c_ItemHeight \ 2))
            End If
            curY += (c_ItemHeight + c_ItemSpacing)
        Next
    End Sub

    Private Sub LineWidthControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics()

        If newSelection <> m_CurrentSelection AndAlso (newSelection <= c_MaxLineWidth) Then
            g.DrawRectangle(New Pen(System.Drawing.Color.White, 2), New Rectangle(c_ItemSpacing, c_ItemSpacing + (m_CurrentSelection) * (c_ItemHeight + c_ItemSpacing), Me.Width - (2 * c_ItemSpacing), c_ItemHeight))
            g.DrawRectangle(New Pen(System.Drawing.Color.DarkGray, 2), New Rectangle(c_ItemSpacing, c_ItemSpacing + (newSelection) * (c_ItemHeight + c_ItemSpacing), Me.Width - (2 * c_ItemSpacing), c_ItemHeight))
            m_CurrentSelection = newSelection
        End If
    End Sub

    Public ReadOnly Property SelectedSize() As Integer
        Get
            Return m_retval
        End Get
    End Property

    Private Sub LineWidthControl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        If (newSelection <= c_MaxLineWidth) Then
            m_retval = newSelection
            Me.Hide()
            m_Provider.CloseDropDown()
        End If
    End Sub
End Class




























