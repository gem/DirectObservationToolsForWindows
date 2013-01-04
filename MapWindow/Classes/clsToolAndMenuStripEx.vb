' Jim Hollenhorst, February 4, 2006
' www.ultrapico.com
' Feel free to duplicate and distribute
' Kudos to Rick Brewster and JasonD

Namespace ToolStripExtensions

    ' The ToolStrip and MenuStrip classes are extended to allow customization of the user interface
    '
    ' The following new boolean properties are exposed in the designer:
    '
    ' ClickThrough - Allow the first click to activate the control, even when the containing form is not active
    ' SupressHighlighting - Suppress the mouseover highlighting of the control when the containing form is not active
    '
    ' The ideas behind this were borrowed from two items found on the Internet:
    '
    ' Rick Brewster shows how to implement ClickThrough on his blog at:
    '   http://blogs.msdn.com/rickbrew/
    '
    ' JasonD suggests the method to suppress the highlighting on at forum at: 
    '   http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=118385&SiteID=1

    ''' <summary>
    ''' Define some Windows message constants
    ''' </summary>
    Public Class WinConst
        Public Const WM_MOUSEMOVE As UInteger = &H200
        Public Const WM_MOUSEACTIVATE As UInteger = &H21
        Public Const MA_ACTIVATE As UInteger = 1
        Public Const MA_ACTIVATEANDEAT As UInteger = 2
        Public Const MA_NOACTIVATE As UInteger = 3
        Public Const MA_NOACTIVATEANDEAT As UInteger = 4
    End Class
    
    ''' <summary>
    ''' This class adds to the functionality provided in System.Windows.Forms.MenuStrip.
    ''' 
    ''' It allows you to "ClickThrough" to the MenuStrip so that you don't have to click once to 
    ''' bring the form into focus and once more to take the desired action
    ''' 
    ''' It also implements a SuppressHighlighting property to turn off the highlighting
    ''' that occures on mouseover when the form is not active
    ''' </summary>
    <System.ComponentModel.DesignerCategory("")> _
    Public Class MenuStripEx
        Inherits MenuStrip
        Private m_clickThrough As Boolean = False
        Private m_suppressHighlighting As Boolean = True

        ''' <summary>
        ''' Gets or sets whether the control honors item clicks when its containing form does
        ''' not have input focus.
        ''' </summary>
        ''' <remarks>
        ''' Default value is false, which is the same behavior provided by the base ToolStrip class.
        ''' </remarks>
        Public Property ClickThrough() As Boolean
            Get
                Return Me.m_clickThrough
            End Get
            Set(ByVal value As Boolean)
                Me.m_clickThrough = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the control shows highlighting on mouseover
        ''' </summary>
        ''' <remarks>
        ''' Default value is true, which is the same behavior provided by the base MenuStrip class.
        ''' </remarks>
        Public Property SuppressHighlighting() As Boolean
            Get
                Return Me.m_suppressHighlighting
            End Get
            Set(ByVal value As Boolean)
                Me.m_suppressHighlighting = value
            End Set
        End Property

        ''' <summary>
        ''' This method overrides the procedure that responds to Windows messages.
        ''' 
        ''' It intercepts the WM_MOUSEMOVE message
        ''' and ignores it if SuppressHighlighting is on and the TopLevelControl does not contain the focus.
        ''' Otherwise, it calls the base class procedure to handle the message.
        ''' 
        ''' It also intercepts the WM_MOUSEACTIVATE message and replaces an "Activate and Eat" result with
        ''' an "Activate" result if ClickThrough is enabled.
        ''' </summary>
        ''' <param name="m"></param>
        Protected Overloads Overrides Sub WndProc(ByRef m As Message)
            ' If we don't want highlighting, throw away mousemove commands
            ' when the parent form or one of its children does not have the focus
            If m.Msg = WinConst.WM_MOUSEMOVE AndAlso Me.m_suppressHighlighting AndAlso Not Me.TopLevelControl.ContainsFocus Then
                Exit Sub
            Else
                MyBase.WndProc(m)
            End If

            ' If we want ClickThrough, replace "Activate and Eat" with "Activate" on WM_MOUSEACTIVATE messages
            If m.Msg = WinConst.WM_MOUSEACTIVATE AndAlso Me.m_clickThrough AndAlso m.Result = WinConst.MA_ACTIVATEANDEAT Then
                m.Result = WinConst.MA_ACTIVATE
            End If
        End Sub
    End Class
    
    ''' <summary>
    ''' This class adds to the functionality provided in System.Windows.Forms.ToolStrip.
    ''' 
    ''' It allows you to "ClickThrough" to the MenuStrip so that you don't have to click once to 
    ''' bring the form into focus and once more to take the desired action
    ''' 
    ''' It also implements a SuppressHighlighting property to turn off the highlighting
    ''' that occures on mouseover when the form is not active
    ''' </summary>
    <System.ComponentModel.DesignerCategory("")> _
    Public Class ToolStripEx
        Inherits ToolStrip
        Private m_clickThrough As Boolean = False
        Private m_suppressHighlighting As Boolean = True

        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Gets or sets whether the control honors item clicks when its containing form does
        ''' not have input focus.
        ''' </summary>
        ''' <remarks>
        ''' Default value is false, which is the same behavior provided by the base ToolStrip class.
        ''' </remarks>
        Public Property ClickThrough() As Boolean
            Get
                Return Me.m_clickThrough
            End Get
            Set(ByVal value As Boolean)
                Me.m_clickThrough = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the control shows highlighting on mouseover
        ''' </summary>
        ''' <remarks>
        ''' Default value is true, which is the same behavior provided by the base MenuStrip class.
        ''' </remarks>
        Public Property SuppressHighlighting() As Boolean
            Get
                Return Me.m_suppressHighlighting
            End Get
            Set(ByVal value As Boolean)
                Me.m_suppressHighlighting = value
            End Set
        End Property

        ''' <summary>
        ''' This method overrides the procedure that responds to Windows messages.
        ''' 
        ''' It intercepts the WM_MOUSEMOVE message
        ''' and ignores it if SuppressHighlighting is on and the TopLevelControl does not contain the focus.
        ''' Otherwise, it calls the base class procedure to handle the message.
        ''' 
        ''' It also intercepts the WM_MOUSEACTIVATE message and replaces an "Activate and Eat" result with
        ''' an "Activate" result if ClickThrough is enabled.
        ''' </summary>
        ''' <param name="m"></param>
        Protected Overloads Overrides Sub WndProc(ByRef m As Message)
            ' If we don't want highlighting, throw away mousemove commands
            ' when the parent form or one of its children does not have the focus
            If m.Msg = WinConst.WM_MOUSEMOVE AndAlso Me.m_suppressHighlighting AndAlso Not Me.TopLevelControl Is Nothing AndAlso Not Me.TopLevelControl.ContainsFocus Then
                Exit Sub
            Else
                MyBase.WndProc(m)
            End If

            ' If we want ClickThrough, replace "Activate and Eat" with "Activate" on WM_MOUSEACTIVATE messages
            If m.Msg = WinConst.WM_MOUSEACTIVATE AndAlso Me.m_clickThrough AndAlso m.Result = WinConst.MA_ACTIVATEANDEAT Then
                m.Result = WinConst.MA_ACTIVATE
            End If
        End Sub
    End Class
End Namespace
