Imports System.Windows.Forms.Design

Friend Class FillStyleControl
    Inherits System.Windows.Forms.UserControl

    Private Const c_ItemSpacing As Integer = 2
    Private Const c_ItemHeight As Integer = 30
    Private Const c_NumFillStyles As Integer = 5
    Private m_CurrentItem As Integer = -1
    Private m_Provider As IWindowsFormsEditorService
    Private m_retval As MapWindow.FillSyles = FillSyles.None

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
        'FillStyleControl
        '
        Me.Name = "FillStyleControl"
        Me.Size = New System.Drawing.Size(100, 194)

    End Sub

#End Region

    Private Sub FillStyleControl_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim br As System.Drawing.Brush
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim outlinePen As Pen = New Pen(System.Drawing.Color.Black)
        Dim box As Rectangle = New Rectangle(2 * c_ItemSpacing, 2 * c_ItemSpacing, e.ClipRectangle.Width - 4 * c_ItemSpacing, c_ItemHeight - 2 * c_ItemSpacing)

        br = New System.Drawing.SolidBrush(System.Drawing.Color.White)
        g.FillRectangle(br, box)
        g.DrawRectangle(outlinePen, box)
        box.Y += c_ItemHeight + c_ItemSpacing

        br.Dispose()
        br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.Vertical, System.Drawing.Color.Black, System.Drawing.Color.White)
        g.FillRectangle(br, box)
        g.DrawRectangle(outlinePen, box)
        box.Y += c_ItemHeight + c_ItemSpacing

        br.Dispose()
        br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.Horizontal, System.Drawing.Color.Black, System.Drawing.Color.White)
        g.FillRectangle(br, box)
        g.DrawRectangle(outlinePen, box)
        box.Y += c_ItemHeight + c_ItemSpacing

        br.Dispose()
        br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.ForwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.White)
        g.FillRectangle(br, box)
        g.DrawRectangle(outlinePen, box)
        box.Y += c_ItemHeight + c_ItemSpacing

        br.Dispose()
        br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.BackwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.White)
        g.FillRectangle(br, box)
        g.DrawRectangle(outlinePen, box)
        box.Y += c_ItemHeight + c_ItemSpacing

        br.Dispose()
        br = New System.Drawing.Drawing2D.HatchBrush(Drawing.Drawing2D.HatchStyle.DottedGrid, System.Drawing.Color.Black, System.Drawing.Color.White)
        g.FillRectangle(br, box)
        g.DrawRectangle(outlinePen, box)

        br.Dispose()
        outlinePen.Dispose()
    End Sub

    Private Sub FillStyleControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim g As System.Drawing.Graphics = Me.CreateGraphics()
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        If (newSelection <> m_CurrentItem) AndAlso (newSelection <= c_NumFillStyles) Then
            g.DrawRectangle(New Pen(System.Drawing.Color.White), New Rectangle(2, m_CurrentItem * (c_ItemHeight + c_ItemSpacing) + c_ItemSpacing, Me.Width - 2 * c_ItemSpacing, c_ItemHeight))
            m_CurrentItem = newSelection
            g.DrawRectangle(New Pen(System.Drawing.Color.DarkGray), New Rectangle(2, newSelection * (c_ItemHeight + c_ItemSpacing) + c_ItemSpacing, Me.Width - 2 * c_ItemSpacing, c_ItemHeight))
        End If
    End Sub

    Private Sub FillStyleControl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        If (newSelection <= c_NumFillStyles) Then
            m_retval = CType(newSelection, MapWindow.FillSyles)
            Me.Hide()
            m_Provider.CloseDropDown()
        End If
    End Sub

    Public ReadOnly Property SelectedValue() As MapWindow.FillSyles
        Get
            Return m_retval
        End Get
    End Property
End Class
