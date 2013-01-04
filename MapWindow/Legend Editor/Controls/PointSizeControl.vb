Imports System.Windows.Forms.Design

Friend Class PointSizeControl
    Inherits System.Windows.Forms.UserControl


    Private Const c_ItemHeight As Integer = 20
    Private Const c_ItemSpacing As Integer = 2
    Private Const c_MaxLineWidth As Integer = 9
    Private m_CurrentSelection As Integer = -1
    Private m_retval As Double = -1
    Private m_Provider As IWindowsFormsEditorService
    Private m_PointType As MapWinGIS.tkPointType

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal DialogProvider As IWindowsFormsEditorService, ByVal pointType As MapWinGIS.tkPointType, ByVal InitialValue As Double)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Provider = DialogProvider
        m_PointType = pointType
        m_retval = InitialValue
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
        'PointSizeControl
        '
        Me.Name = "PointSizeControl"
        Me.Size = New System.Drawing.Size(44, 224)

    End Sub

#End Region

    Private Sub LineWidthControl_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim g As System.Drawing.Graphics = e.Graphics
        Dim tBrush As New Drawing.SolidBrush(Color.Black)
        Dim tri(2) As Point, diamond(3) As Point
        Dim midX, midY As Single
        Dim i, curY As Integer
        Dim rect As Rectangle

        curY = c_ItemSpacing

        For i = 1 To 10
            curY += c_ItemHeight + c_ItemSpacing

            midY = CSng(curY + c_ItemHeight / 2)
            midX = CSng(Me.ClientRectangle.Width / 2)
            rect = New Rectangle(CInt(midX - i / 2), CInt(midY - i / 2), i, i)

            Select Case m_PointType
                Case MapWinGIS.tkPointType.ptSquare
                    g.FillRectangle(tBrush, rect)

                Case MapWinGIS.tkPointType.ptCircle
                    g.FillEllipse(tBrush, rect)

                Case MapWinGIS.tkPointType.ptDiamond
                    diamond(0).X = CInt(midX)
                    diamond(0).Y = rect.Top
                    diamond(1).X = rect.Left + rect.Width
                    diamond(1).Y = CInt(midY)
                    diamond(2).X = CInt(midX)
                    diamond(2).Y = rect.Top + rect.Height
                    diamond(3).X = rect.Left
                    diamond(3).Y = CInt(midY)
                    g.FillPolygon(tBrush, diamond)

                Case MapWinGIS.tkPointType.ptTriangleUp
                    tri(0).X = rect.Left
                    tri(0).Y = rect.Top + rect.Height
                    tri(1).X = CInt(midX)
                    tri(1).Y = rect.Top
                    tri(2).X = rect.Left + rect.Width
                    tri(2).Y = rect.Top + rect.Height
                    g.FillPolygon(tBrush, tri)

                Case MapWinGIS.tkPointType.ptTriangleDown
                    tri(0).X = rect.Left
                    tri(0).Y = rect.Top
                    tri(1).X = rect.Left + rect.Width
                    tri(1).Y = rect.Top
                    tri(2).X = CInt(midX)
                    tri(2).Y = rect.Top + rect.Height
                    g.FillPolygon(tBrush, tri)

                Case MapWinGIS.tkPointType.ptTriangleLeft
                    tri(0).X = rect.Left
                    tri(0).Y = CInt(midY)
                    tri(1).X = rect.Left + rect.Width
                    tri(1).Y = rect.Top
                    tri(2).X = rect.Left + rect.Width
                    tri(2).Y = rect.Top + rect.Height
                    g.FillPolygon(tBrush, tri)

                Case MapWinGIS.tkPointType.ptTriangleRight
                    tri(0).X = rect.Left
                    tri(0).Y = rect.Top
                    tri(1).X = rect.Left + rect.Width
                    tri(1).Y = CInt(midY)
                    tri(2).X = rect.Left
                    tri(2).Y = rect.Top + rect.Height
                    g.FillPolygon(tBrush, tri)

            End Select
        Next
        tBrush.Dispose()
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
            Return CInt(m_retval)
        End Get
    End Property

    Private Sub LineWidthControl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Dim newSelection As Integer = CInt(e.Y \ (c_ItemSpacing + c_ItemHeight))
        If (newSelection <= c_MaxLineWidth) Then
            m_retval = newSelection
            Me.Hide()
            m_Provider.CloseDropDown()
        End If
    End Sub

End Class
