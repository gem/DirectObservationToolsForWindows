Public Class RangeBar
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.SetStyle(ControlStyles.DoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint, True)
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
        'RangeBar
        '
        Me.Name = "RangeBar"
        Me.Size = New System.Drawing.Size(272, 16)

    End Sub

#End Region

	Event UserDraggingHandle(ByVal aHandle As Integer, ByVal aValue As Double)
	Event UserDraggedHandle(ByVal aHandle As Integer, ByVal aValue As Double)

	Private pHandleColors() As Color = {}	 'color of the range from this handle to the next higher one
	Private pHandleValues() As Double = {}	 'value associated with each handle
	Private pHandlePix() As Integer = {}	 'pixel position of each handle
	Private pLastHandle As Integer = -1	  'maximum handle index

	Private pHistogram() As Integer = {}	 'histogram of values
	Private pLastBin As Integer = -1		 'last bin index in pHistogram
    Private pHistogramMinValue As Double = 0  'value associate with bottom of first bin
    Private pHistogramMaxValue As Double = 0  'value associate with top of last bin
    Private pHistogramMaxPerBin As Integer = 0  'maximum value in a histogram bin

	Private pHandleDragging As Integer = -1		'index of handle currently being dragged
	Private pHandleDragging2 As Integer = -1	'index of last handle currently being dragged
	Private pCursorToHandle As Integer = 0
	Private pDraggableTolerance As Integer = 3	'pixel distance allowed for draggable cursor feedback
	Private pAllowDragMin As Boolean = False
	Private pAllowDragMax As Boolean = False

    Public Sub HistogramFromValues(ByVal aValues() As Double, Optional ByVal aMinValue As Double = Double.NaN, _
                                                              Optional ByVal aMaxValue As Double = Double.NaN)
        Dim lLastValue As Integer = aValues.GetUpperBound(0)
        pLastBin = CInt(Me.Width / 4)
        ReDim pHistogram(pLastBin)
        Dim lBin As Integer

        If Double.IsNaN(aMinValue) OrElse Double.IsNaN(aMaxValue) Then
            aMinValue = aValues(0)
            aMaxValue = aValues(0)
            For lValueIndex As Integer = 1 To lLastValue
                aMinValue = Math.Min(aMinValue, aValues(lValueIndex))
                aMaxValue = Math.Max(aMaxValue, aValues(lValueIndex))
            Next
        End If

        pHistogramMinValue = aMinValue
        pHistogramMaxValue = aMaxValue

        Dim lBinsPerValue As Double = pLastBin / (aMaxValue - aMinValue)
        For lValueIndex As Integer = 0 To lLastValue
            lBin = CInt((aValues(lValueIndex) - aMinValue) * lBinsPerValue)
            pHistogram(lBin) += 1
        Next

        FindHistogramMaxPerBin()
    End Sub

    Public Property Histogram() As Integer()
        Get
            Return pHistogram
        End Get
        Set(ByVal newValue As Integer())
            pHistogram = newValue
            pLastBin = pHistogram.GetUpperBound(0)
            FindHistogramMaxPerBin()
        End Set
    End Property

    Public ReadOnly Property HistogramMaxPerBin() As Integer
        Get
            Return pHistogramMaxPerBin
        End Get
    End Property

    Private Sub FindHistogramMaxPerBin()
        pHistogramMaxPerBin = 0
        For lBin As Integer = 0 To pLastBin
            pHistogramMaxPerBin = Math.Max(pHistogramMaxPerBin, pHistogram(lBin))
        Next
    End Sub

    Public Sub SetHandles(ByVal aValues() As Double, ByVal aColors() As Color)
        pHandleValues = aValues
        pHandleColors = aColors
        pLastHandle = pHandleValues.GetUpperBound(0)
        'TODO: Test for pHandleColors.GetUpperBound(0) = pLastHandle?
        ReDim pHandlePix(pLastHandle)
    End Sub

    Public ReadOnly Property HandleValue(ByVal aIndex As Integer) As Double
        Get
            If aIndex >= 0 AndAlso aIndex <= pLastHandle Then
                Return pHandleValues(aIndex)
            End If
        End Get
    End Property

    Public ReadOnly Property HandleColor(ByVal aIndex As Integer) As Color
        Get
            If aIndex >= 0 AndAlso aIndex <= pLastHandle Then
                Return pHandleColors(aIndex)
            End If
        End Get
    End Property

    Private Sub RangeBar_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Me.Render(e.Graphics)
    End Sub

    Private Sub Render(ByVal g As Graphics)
        Try
            If Me.Visible AndAlso pHandleValues.Length > 0 Then
                Dim x As Single = 0
                Dim y As Single = 0

                Dim lMaxValue As Double = pHandleValues(pLastHandle)
                Dim lMinValue As Double = pHandleValues(0)
                Dim lValueWidth As Double = lMaxValue - lMinValue

                Dim visibleHeight As Integer = Me.Height
                Dim visibleWidth As Integer = Me.Width

                Dim pixPerValue As Double = visibleWidth / lValueWidth

                Dim lLinePen As New Pen(System.Drawing.Color.Black, 1)
                Dim lRangeBrush As SolidBrush

                Dim lValue As Double = lMinValue
                Dim lNextValue As Double

                Dim lPix As Integer = 0
                Dim lNextPix As Integer
                For lRange As Integer = 0 To pLastHandle - 1
                    lRangeBrush = New SolidBrush(pHandleColors(lRange))
                    lNextValue = pHandleValues(lRange + 1)
                    lNextPix = CInt((lNextValue - lMinValue) * pixPerValue)
                    g.FillRectangle(lRangeBrush, lPix, 0, lNextPix, visibleHeight)
                    g.DrawLine(lLinePen, lPix, 0, lPix, visibleHeight)
                    pHandlePix(lRange) = lPix
                    lValue = lNextValue
                    lPix = lNextPix
                Next
                g.DrawLine(lLinePen, lPix, 0, lPix, visibleHeight)
                pHandlePix(pLastHandle) = lPix
                If pHistogramMaxPerBin > 0 Then
                    RenderHistogram(g)
                End If
            End If
        Catch e As System.OverflowException
            'Do nothing
        Catch e As Exception
            modMain.CustomExceptionHandler.OnThreadException(e)
        End Try
    End Sub

    Private Sub RenderHistogram(ByVal g As Graphics)
        'TODO: if pHistogramMinValue <> pHandleValues(0) 
        '      or pHistogramMaxValue <> pHandleValues(pLastHandle), rescale x-axis of histogram
        Dim lBrush As New SolidBrush(Color.Black)
        Dim lBarBottom As Integer = Me.Height
        Dim lMaxBarHeight As Integer = CInt(lBarBottom * 0.95)
        Dim lHeightFactor As Double = lMaxBarHeight / pHistogramMaxPerBin
        Dim lPixPerBin As Double = Me.Width / (pLastBin + 1)
        Dim lBarWidth As Integer = CInt(lPixPerBin / 2)
        If lBarWidth < 1 Then lBarWidth = 1
        For lBin As Integer = 0 To pLastBin
            Dim lBarLeft As Integer = CInt(lPixPerBin * lBin)
            Dim lBarHeight As Integer = CInt(lHeightFactor * pHistogram(lBin))
            'g.FillRectangle(lBrush, lBarLeft, lBarHeight, lBarWidth, lMaxBarHeight - lBarHeight)
            g.FillRectangle(lBrush, lBarLeft + lBarWidth, lBarBottom - lBarHeight, lBarWidth, lBarHeight)
        Next

        'Dim lBrush As New SolidBrush(Color.Black)
        'Dim lBarBottom As Integer = Me.Height
        'Dim lMaxBarHeight As Integer = CInt(lBarBottom * 0.95)
        'Dim lHeightFactor As Double = lMaxBarHeight / pHistogramMaxPerBin
        'Dim lPixPerBin As Double = Me.Width / (pLastBin + 1)
        'Dim lBarWidth As Integer = CInt(lPixPerBin / 2)
        'If lBarWidth < 1 Then lBarWidth = 1
        'For lBin As Integer = 0 To pLastBin
        '    Dim lBarLeft As Integer = CInt(lPixPerBin * lBin)
        '    Dim lBarHeight As Integer = CInt(lHeightFactor * pHistogram(lBin))
        '    g.FillRectangle(lBrush, lBarLeft, lBarBottom, lBarWidth, lBarBottom - lBarHeight)
        'Next

    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim lHandle As Integer = HandleToDrag(e.X)
        Select Case e.Button
            Case MouseButtons.Left
                If lHandle >= 0 Then
                    pHandleDragging = lHandle
                    pCursorToHandle = 0
                Else     'TODO: drag two nearest handles?
                    lHandle = HandleBelow(e.X)
                    If lHandle > -1 AndAlso (pAllowDragMin OrElse lHandle > 0) AndAlso _
                      (pAllowDragMax OrElse lHandle + 1 < pLastHandle) Then
                        pHandleDragging = lHandle
                        pHandleDragging2 = lHandle + 1
                        pCursorToHandle = e.X - pHandlePix(pHandleDragging)
                    End If
                End If
            Case MouseButtons.Right
                'TODO: color selection? Insert handle?
        End Select
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim newCursor As Windows.Forms.Cursor = Cursors.Default
        Select Case e.Button
            Case MouseButtons.None
                If HandleToDrag(e.X) >= 0 Then
                    newCursor = Cursors.SizeWE
                End If
                If Not Me.Cursor Is newCursor Then Me.Cursor = newCursor
            Case MouseButtons.Left
                If pHandleDragging >= 0 Then
                    'Make sure handle is not being dragged past neighbor
                    Dim lNewHandlePix As Integer
                    Dim lNewHandlePix2 As Integer
                    If pHandleDragging2 > -1 Then
                        Dim lMoveDistance As Integer = e.X - pCursorToHandle - pHandlePix(pHandleDragging)
                        lNewHandlePix = pHandlePix(pHandleDragging) + lMoveDistance
                        lNewHandlePix2 = pHandlePix(pHandleDragging2) + lMoveDistance
                    Else
                        lNewHandlePix = e.X
                    End If
                    If (pHandleDragging = 0 OrElse pHandlePix(pHandleDragging - 1) < lNewHandlePix) AndAlso _
                       (pHandleDragging = pLastHandle OrElse pHandlePix(pHandleDragging + 1) > lNewHandlePix) AndAlso _
                       (pHandleDragging2 < 0 OrElse pHandleDragging2 = pLastHandle OrElse pHandlePix(pHandleDragging2 + 1) > lNewHandlePix2) Then
                        If pHandleDragging2 > -1 Then
                            pHandlePix(pHandleDragging2) = lNewHandlePix2
                            pHandleValues(pHandleDragging2) = pHandleValues(0) + lNewHandlePix2 * (pHandleValues(pLastHandle) - pHandleValues(0)) / Me.Width
                            RaiseEvent UserDraggingHandle(pHandleDragging2, pHandleValues(pHandleDragging2))
                        End If
                        pHandlePix(pHandleDragging) = lNewHandlePix
                        pHandleValues(pHandleDragging) = pHandleValues(0) + pHandlePix(pHandleDragging) * (pHandleValues(pLastHandle) - pHandleValues(0)) / Me.Width
                        RaiseEvent UserDraggingHandle(pHandleDragging, pHandleValues(pHandleDragging))
                        Refresh()
                    End If
                End If
        End Select
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If pHandleDragging > -1 Then
            RaiseEvent UserDraggedHandle(pHandleDragging, pHandleValues(pHandleDragging))
            pHandleDragging = -1
        End If
        If pHandleDragging2 > -1 Then
            RaiseEvent UserDraggedHandle(pHandleDragging2, pHandleValues(pHandleDragging2))
            pHandleDragging2 = -1
        End If
    End Sub

    Private Function HandleToDrag(ByVal X As Integer) As Integer
        For lHandle As Integer = 0 To pLastHandle
            'If within tolerance of handle
            If Math.Abs(X - pHandlePix(lHandle)) <= pDraggableTolerance Then
                'Make sure this handle allows dragging
                If (pAllowDragMin OrElse lHandle > 0) AndAlso _
                   (pAllowDragMax OrElse lHandle < pLastHandle) Then
                    Return lHandle
                End If
            End If
        Next
        Return -1
    End Function

    Private Function HandleBelow(ByVal X As Integer) As Integer
        For lHandle As Integer = pLastHandle To 0 Step -1
            If X >= pHandlePix(lHandle) Then
                Return lHandle
            End If
        Next
        Return -1
    End Function

    Private Sub RangeBar_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Refresh()
    End Sub
End Class
