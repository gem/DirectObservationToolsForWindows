Imports System.Windows.Forms.Design

Class LineStyleControl
    Inherits System.Windows.Forms.UserControl

    Private Const c_ItemSpacing As Integer = 2
    Private Const c_ItemHeight As Integer = 30
    Private Const c_NumStyles As Integer = 4
    Private m_CurrentItem As Integer = -1
    Private m_Provider As IWindowsFormsEditorService
    Private m_retval As MapWindow.LineStyles = LineStyles.Solid

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
    End Sub
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
        'LineStyleControl
        '
        Me.Name = "LineStyleControl"
        Me.Size = New System.Drawing.Size(150, 130)

    End Sub

#End Region



    Private Sub LineStyleControl_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim myPen As System.Drawing.Pen = New Pen(System.Drawing.Color.Black)
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim box As Rectangle = New Rectangle(2 * c_ItemSpacing, 2 * c_ItemSpacing, e.ClipRectangle.Width - 4 * c_ItemSpacing, c_ItemHeight - 2 * c_ItemSpacing)
        Dim outlinePen As Pen = New Pen(System.Drawing.Color.Black)

        Dim ar() As Single = {0.0, 0.3, 0.6, 1.0}

        myPen.CompoundArray = ar
        myPen.Width = 2
        myPen.DashStyle = Drawing.Drawing2D.DashStyle.Solid
        g.DrawRectangle(outlinePen, box)
        g.DrawLine(myPen, box.Left + c_ItemSpacing, (box.Top + box.Bottom) \ 2, box.Right - c_ItemSpacing, (box.Top + box.Bottom) \ 2)
        box.Y += c_ItemHeight + c_ItemSpacing

        myPen.DashStyle = Drawing.Drawing2D.DashStyle.Dot
        g.DrawRectangle(outlinePen, box)
        g.DrawLine(myPen, box.Left + c_ItemSpacing, (box.Top + box.Bottom) \ 2, box.Right - c_ItemSpacing, (box.Top + box.Bottom) \ 2)
        box.Y += c_ItemHeight + c_ItemSpacing

        myPen.DashStyle = Drawing.Drawing2D.DashStyle.Dash
        g.DrawRectangle(outlinePen, box)
        g.DrawLine(myPen, box.Left + c_ItemSpacing, (box.Top + box.Bottom) \ 2, box.Right - c_ItemSpacing, (box.Top + box.Bottom) \ 2)
        box.Y += c_ItemHeight + c_ItemSpacing

        myPen.DashStyle = Drawing.Drawing2D.DashStyle.DashDot
        g.DrawRectangle(outlinePen, box)
        g.DrawLine(myPen, box.Left + c_ItemSpacing, (box.Top + box.Bottom) \ 2, box.Right - c_ItemSpacing, (box.Top + box.Bottom) \ 2)
        box.Y += c_ItemHeight + c_ItemSpacing

        outlinePen.Dispose()
        myPen.Dispose()
    End Sub

    Private Sub LineStyleControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim g As System.Drawing.Graphics = Me.CreateGraphics()
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        If (newSelection <> m_CurrentItem) AndAlso (newSelection < c_NumStyles) Then
            g.DrawRectangle(New Pen(System.Drawing.Color.White), New Rectangle(2, m_CurrentItem * (c_ItemHeight + c_ItemSpacing) + c_ItemSpacing, Me.Width - 2 * c_ItemSpacing, c_ItemHeight))
            m_CurrentItem = newSelection
            g.DrawRectangle(New Pen(System.Drawing.Color.DarkGray), New Rectangle(2, newSelection * (c_ItemHeight + c_ItemSpacing) + c_ItemSpacing, Me.Width - 2 * c_ItemSpacing, c_ItemHeight))
        End If
    End Sub

    Private Sub LineStyleControl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        If (newSelection <= c_NumStyles) Then
            m_retval = CType(newSelection, MapWindow.LineStyles)
            Me.Hide()
            m_Provider.CloseDropDown()
        End If
    End Sub

    Public ReadOnly Property SelectedValue() As MapWindow.LineStyles
        Get
            Return m_retval
        End Get
    End Property
End Class



'End Class
