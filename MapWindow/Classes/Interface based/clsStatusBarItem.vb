Public Class StatusBarItem
    Implements Interfaces.StatusBarItem

    Friend m_item As ToolStripItem = Nothing

    Friend Sub New(ByVal item As ToolStripItem)
        If item Is Nothing Then Throw New ArgumentException("Not reference to toolstrip item was specified")
        m_item = item
    End Sub

    <CLSCompliant(False)> _
    Public Property Alignment() As Interfaces.eAlignment Implements Interfaces.StatusBarItem.Alignment
        Get
            Try
                Select Case m_item.Alignment ' frmMain.StatusBar.Item(m_Index).Alignment
                    Case HorizontalAlignment.Center
                        Return Interfaces.eAlignment.Center
                    Case HorizontalAlignment.Left
                        Return Interfaces.eAlignment.Left
                    Case HorizontalAlignment.Right
                        Return Interfaces.eAlignment.Right
                End Select
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Get
        Set(ByVal Value As Interfaces.eAlignment)
            Try
                'frmMain.StatusBar.Item(m_Index)
                m_item.Alignment = CType(Value, System.Windows.Forms.HorizontalAlignment)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Set
    End Property

    Public Property AutoSize() As Boolean Implements Interfaces.StatusBarItem.AutoSize
        Get
            Try
                Return CBool(m_item.AutoSize)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Get
        Set(ByVal Value As Boolean)
            Try
                m_item.AutoSize = IIf(Value, StatusBarPanelAutoSize.Spring, StatusBarPanelAutoSize.None)
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Set
    End Property

    Public Property MinWidth() As Integer Implements Interfaces.StatusBarItem.MinWidth
        Get
            Try
                Return m_item.Width
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Get
        Set(ByVal Value As Integer)
            Try
                m_item.Width = Value
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Set
    End Property

    Public Property Text() As String Implements Interfaces.StatusBarItem.Text
        Get
            Try
                Return m_item.Text
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
                Return ""
            End Try
        End Get
        Set(ByVal Value As String)
            Try
                m_item.Text = Value
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Set
    End Property

    Public Property Width() As Integer Implements Interfaces.StatusBarItem.Width
        Get
            Try
                Return m_item.Width
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Get
        Set(ByVal Value As Integer)
            Try
                m_item.Width = Value
            Catch ex As Exception
                g_error = ex.Message
                ShowError(ex)
            End Try
        End Set
    End Property


End Class
