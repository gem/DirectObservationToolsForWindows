'Chris Michaelis, Feb 2008
'
'This is just a trick to create a "non-modal" message box in VB.Net.
'Technically, the message box is still modal.... but it's modal to a new
'thread that's separate from the main one, which can still get focus
'and be interacted with.
Module modNonModalMessageBox
    Public Sub doNonModalMessageBox(ByVal Message As String, ByVal msgboxStyle As MsgBoxStyle, ByVal Caption As String)
        m_Message = Message
        m_Caption = Caption
        m_Style = msgboxStyle
        Dim thrd As New Threading.Thread(AddressOf doNonModalMessageBox__)
        thrd.Start()
        thrd = Nothing
    End Sub

    Private m_Message As String
    Private m_Style As MsgBoxStyle
    Private m_Caption As String
    Private Sub doNonModalMessageBox__()
        'TODO: MapWinUtility.Logger cannot handle multiple threads, would be nice to have a multithreaded logger!!!
        'Issue: the logger needs to know about the main mapwindow form for status messages.
        'MapWinUtility.Logger.Msg(m_Message, m_Style, m_Caption)
        MsgBox(m_Message, m_Style, m_Caption)
    End Sub
End Module
