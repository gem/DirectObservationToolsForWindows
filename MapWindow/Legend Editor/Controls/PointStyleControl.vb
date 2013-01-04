Imports System.Windows.Forms.Design

Friend Class PointStyleControl
    Inherits System.Windows.Forms.UserControl

    Private Const c_ItemSpacing As Integer = 2
    Private Const c_ItemHeight As Integer = 20
    Private Const c_NumStyles As Integer = 7
    Private m_CurrentItem As Integer = -1
    Private m_Provider As IWindowsFormsEditorService
    Private m_retval As MapWindow.PointStyles

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
        'PointStyleControl
        '
        Me.Name = "PointStyleControl"
        Me.Size = New System.Drawing.Size(44, 156)

    End Sub

#End Region

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
    End Sub

    Private Sub PointStyleControl_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim box As Rectangle
        Dim tPen As New Pen(System.Drawing.Color.Black)
        Dim tBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Black)
        Dim tri(2) As Point, diamond(3) As Point
        Dim boxSize As Integer = c_ItemHeight - (c_ItemSpacing * 2)
        Dim boxTop As Integer = 2 * c_ItemSpacing
        Dim boxLeft As Integer = (e.ClipRectangle.Width \ 2) - (boxSize \ 2)
        Dim midX, midY As Integer

        If e.ClipRectangle.Width < boxSize Then Exit Sub

        box = New Rectangle(boxLeft, boxTop, boxSize, boxSize)
        midX = boxLeft + (box.Width \ 2)

        ' Case PointStyles.Square
        g.FillRectangle(tBrush, box)
        boxTop += c_ItemHeight + c_ItemSpacing
        box = New Rectangle(boxLeft, boxTop, boxSize, boxSize)

        ' Case PointStyles.Circle
        g.FillEllipse(tBrush, box)
        boxTop += c_ItemHeight + c_ItemSpacing
        box = New Rectangle(boxLeft, boxTop, boxSize, boxSize)
        midY = boxTop + (box.Height \ 2)

        ' Case PointStyles.Diamond
        diamond(0).X = midX
        diamond(0).Y = box.Top
        diamond(1).X = boxLeft + box.Width
        diamond(1).Y = midY
        diamond(2).X = midX
        diamond(2).Y = box.Top + box.Height
        diamond(3).X = boxLeft
        diamond(3).Y = midY
        g.FillPolygon(tBrush, diamond)
        boxTop += c_ItemHeight + c_ItemSpacing
        box = New Rectangle(boxLeft, boxTop, boxSize, boxSize)

        ' Case PointStyles.TriangleUp
        tri(0).X = boxLeft
        tri(0).Y = boxTop + box.Height
        tri(1).X = midX
        tri(1).Y = boxTop
        tri(2).X = boxLeft + box.Width
        tri(2).Y = boxTop + box.Height
        g.FillPolygon(tBrush, tri)
        boxTop += c_ItemHeight + c_ItemSpacing
        box = New Rectangle(boxLeft, boxTop, boxSize, boxSize)

        ' Case PointStyles.TriangleDown
        tri(0).X = boxLeft
        tri(0).Y = box.Top
        tri(1).X = boxLeft + box.Width
        tri(1).Y = box.Top
        tri(2).X = midX
        tri(2).Y = box.Top + box.Height
        g.FillPolygon(tBrush, tri)
        boxTop += c_ItemHeight + c_ItemSpacing
        box = New Rectangle(boxLeft, boxTop, boxSize, boxSize)
        midY = boxTop + (box.Height \ 2)

        ' Case PointStyles.TriangleLeft
        tri(0).X = boxLeft
        tri(0).Y = midY
        tri(1).X = boxLeft + box.Width
        tri(1).Y = boxTop
        tri(2).X = boxLeft + box.Width
        tri(2).Y = boxTop + box.Height
        g.FillPolygon(tBrush, tri)
        boxTop += c_ItemHeight + c_ItemSpacing
        box = New Rectangle(boxLeft, boxTop, boxSize, boxSize)
        midY = boxTop + (box.Height \ 2)

        ' Case PointStyles.TriangleRight
        tri(0).X = boxLeft
        tri(0).Y = boxTop
        tri(1).X = boxLeft + box.Width
        tri(1).Y = midY
        tri(2).X = boxLeft
        tri(2).Y = boxTop + box.Height
        g.FillPolygon(tBrush, tri)

        tPen.Dispose()
        tBrush.Dispose()
    End Sub

    Private Sub PointStyleControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim g As System.Drawing.Graphics = Me.CreateGraphics()
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        If (newSelection <> m_CurrentItem) AndAlso (newSelection < c_NumStyles) Then
            g.DrawRectangle(New Pen(System.Drawing.Color.White), New Rectangle(2, m_CurrentItem * (c_ItemHeight + c_ItemSpacing) + c_ItemSpacing, Me.Width - 2 * c_ItemSpacing, c_ItemHeight))
            m_CurrentItem = newSelection
            g.DrawRectangle(New Pen(System.Drawing.Color.DarkGray), New Rectangle(2, newSelection * (c_ItemHeight + c_ItemSpacing) + c_ItemSpacing, Me.Width - 2 * c_ItemSpacing, c_ItemHeight))
        End If
    End Sub

    Private Sub PointStyleControl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Dim newSelection As Integer = e.Y \ (c_ItemSpacing + c_ItemHeight)
        If (newSelection <= c_NumStyles) Then
            m_retval = CType(newSelection, MapWindow.PointStyles)
            Me.Hide()
            m_Provider.CloseDropDown()
        End If
    End Sub

    Public ReadOnly Property SelectedValue() As MapWindow.PointStyles
        Get
            Return m_retval
        End Get
    End Property
End Class
