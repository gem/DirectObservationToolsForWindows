'********************************************************************************************************
'File Name: GlobalizedTypeConverter.vb
'Description: Main class for internationalization of property grids (Settings, Legend Editor).
'********************************************************************************************************

' (c) 2004 Wout de Zeeuw
' http://www.codeproject.com/KB/cs/wdzpropertygridutils.aspx
' Please also refer to:
'
' http://www.codeguru.com/Csharp/Csharp/cs_controls/propertygrid/comments.php/c4795
'
' Dynamic behaviour designed/implemented by George Soules and Wout de Zeeuw.
'
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'5/6/2008 - Initial version created by Jiri Kadlec. Imported from the "Globalized Property Grid" 
'           open-source code by Wout de Zeeuw. Other authors of the original code are Gerd Klevesaat
'           and George Soules. 

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Reflection
Imports System.Resources

Namespace PropertyGridUtils
    ''' <summary>
    ''' This class makes it possible to:
    ''' globalize property display names and desriptions,
    ''' sort properties and set the DisplayName to a property with an attribute.
    ''' Also all property characteristics can be changed dynamically if needed.
    ''' </summary>
    ''' <remarks>
    ''' Set a class's <see cref="TypeConverter"/> to this class to enable
    ''' globalization and dynamic property characteristics.
    ''' <para/>
    ''' The display name and description for a property are obtained
    ''' from a resource file if specified by the <see cref="GlobalizedPropertyAttribute"/>.
    ''' The resource file is expected to be named namespace.classname.resources.
    ''' Resources are expected to be named PropertyName.DisplayName,
    ''' PropertyName.Description PropertyName.Category (all optional).
    ''' Also sorts using the <see cref="PropertyOrderAttribute"/> attribute.
    ''' <para/>
    ''' Specify <see cref="DisplayNameAttribute"/> for simpler usage (not globalized).
    ''' <para/>
    ''' If a property has the <see cref="PropertyAttributesProviderAttribute"/> attribute
    ''' then the delegate specified is called for its runtime <see cref="PropertyAttributes"/>.
    ''' <para/>
    ''' See also
    ''' <seealso cref="DisplayNameAttribute"/>
    ''' <seealso cref="GlobalizedPropertyAttribute"/>
    ''' <seealso cref="GlobalizedTypeAttribute"/>
    ''' <seealso cref="PropertyAttributesProviderAttribute"/>
    ''' <seealso cref="PropertyOrderAttribute"/>     
    ''' </remarks>
    Public Class GlobalizedTypeConverter
        Inherits ExpandableObjectConverter
        Public Overloads Overrides Function GetProperties(ByVal context As ITypeDescriptorContext, ByVal value As Object, ByVal attributes As Attribute()) As PropertyDescriptorCollection
            ' Get the collection of properties
            Dim baseProps As PropertyDescriptorCollection = TypeDescriptor.GetProperties(value, attributes)
            Dim deluxeProps As New PropertyDescriptorCollection(Nothing)

            ' For each property use a property descriptor of
            ' our own that has custom behaviour.
            Dim orderedPropertyAttributesList As New ArrayList()
            For Each oProp As PropertyDescriptor In baseProps
                Dim propertyAttributes As PropertyAttributes = GetPropertyAttributes(oProp, value)

                If propertyAttributes.IsBrowsable Then
                    orderedPropertyAttributesList.Add(propertyAttributes)
                    deluxeProps.Add(New PropertyDescriptorEx(oProp, propertyAttributes))
                End If
            Next
            orderedPropertyAttributesList.Sort()
            '
            ' Build a string list of the ordered names
            '
            Dim propertyNames As New ArrayList()
            For Each propertyAttributes As PropertyAttributes In orderedPropertyAttributesList
                propertyNames.Add(propertyAttributes.Name)
            Next
            '
            ' Pass in the ordered list for the PropertyDescriptorCollection to sort by.
            ' (Sorting by passing a custom IComparer somehow doesn't work.
            '
            Return deluxeProps.Sort(DirectCast(propertyNames.ToArray(GetType(String)), String()))
        End Function

        ''' <summary>
        ''' Get property attributes for given property descriptor and target object.
        ''' </summary>
        Private Function GetPropertyAttributes(ByVal propertyDescriptor As PropertyDescriptor, ByVal target As Object) As PropertyAttributes
            Dim propertyAttributes As New PropertyAttributes(propertyDescriptor.Name)
            Dim resourceBaseName As String = Nothing
            Dim displayName As String = Nothing
            Dim displayNameResourceName As String = Nothing
            Dim descriptionResourceName As String = Nothing
            Dim categoryResourceName As String = Nothing
            Dim rm As ResourceManager = Nothing

            '
            ' First fill propertyAttributes with statically defined information.
            '

            For Each attrib As Attribute In propertyDescriptor.Attributes
                Dim type As Type = attrib.[GetType]()
                ' If there's a DisplayNameAttribute defined, use that DisplayName.
                If type.Equals(GetType(DisplayNameAttribute)) Then
                    displayName = DirectCast(attrib, DisplayNameAttribute).DisplayName
                ElseIf type.Equals(GetType(GlobalizedPropertyAttribute)) Then
                    ' Get specific info about where to find resources for given property.
                    displayNameResourceName = DirectCast(attrib, GlobalizedPropertyAttribute).DisplayNameId
                    descriptionResourceName = DirectCast(attrib, GlobalizedPropertyAttribute).DescriptionId
                    categoryResourceName = DirectCast(attrib, GlobalizedPropertyAttribute).CategoryId
                    resourceBaseName = DirectCast(attrib, GlobalizedPropertyAttribute).BaseName
                ElseIf type.Equals(GetType(PropertyOrderAttribute)) Then
                    propertyAttributes.Order = DirectCast(attrib, PropertyOrderAttribute).Order
                End If
            Next

            If resourceBaseName Is Nothing Then
                For Each attrib As Attribute In propertyDescriptor.ComponentType.GetCustomAttributes(True)
                    If attrib.[GetType]().Equals(GetType(GlobalizedTypeAttribute)) Then
                        ' Get specific info about where to find resources for given Type.
                        resourceBaseName = DirectCast(attrib, GlobalizedTypeAttribute).BaseName
                    End If
                Next
                If resourceBaseName Is Nothing Then
                    resourceBaseName = propertyDescriptor.ComponentType.[Namespace] + "." + propertyDescriptor.ComponentType.Name
                End If
            End If

            ' See if at least the culture neutral resources are there.
            ' If not, disable globalization
            Dim assembly As Assembly = propertyDescriptor.ComponentType.Assembly
            'Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            If assembly.GetManifestResourceInfo(resourceBaseName + ".resources") Is Nothing Then
                rm = Nothing
            Else
                'System.Windows.Forms.MessageBox.Show("resourceBaseName: " + resourceBaseName);
                rm = New ResourceManager(resourceBaseName, assembly)
                If displayNameResourceName Is Nothing Then
                    'displayNameResourceName =
                    ' propertyDescriptor.DisplayName + ".DisplayName";
                    displayNameResourceName = propertyDescriptor.DisplayName
                End If
                If descriptionResourceName Is Nothing Then
                    'descriptionResourceName =
                    ' propertyDescriptor.DisplayName + ".Description";
                    descriptionResourceName = displayNameResourceName + "Description"
                End If
                If categoryResourceName Is Nothing Then
                    categoryResourceName = propertyDescriptor.Category + ".Category"
                End If
            End If

            ' Display name.
            If rm IsNot Nothing Then
                propertyAttributes.DisplayName = rm.GetString(displayNameResourceName)
            Else
                propertyAttributes.DisplayName = Nothing
            End If
            If propertyAttributes.DisplayName Is Nothing Then
                propertyAttributes.DisplayName = displayName
            End If
            If propertyAttributes.DisplayName Is Nothing Then
                propertyAttributes.DisplayName = propertyDescriptor.DisplayName
            End If

            ' Description.
            If rm IsNot Nothing Then
                propertyAttributes.Description = rm.GetString(descriptionResourceName)
            Else
                propertyAttributes.Description = Nothing
            End If
            If propertyAttributes.Description Is Nothing Then
                propertyAttributes.Description = propertyDescriptor.Description
            End If

            ' Category.
            If rm IsNot Nothing Then
                propertyAttributes.Category = rm.GetString(categoryResourceName)
            Else
                propertyAttributes.Category = Nothing
            End If
            If propertyAttributes.Category Is Nothing Then
                propertyAttributes.Category = propertyDescriptor.Category
            End If

            ' IsReadonly.
            propertyAttributes.IsReadOnly = propertyDescriptor.IsReadOnly

            ' IsBrowsable.
            propertyAttributes.IsBrowsable = propertyDescriptor.IsBrowsable

            '
            ' Now let target be able to override each of these property attributes
            ' dynamically.
            '

            Dim propertyAttributesProviderAttribute As PropertyAttributesProviderAttribute = DirectCast(propertyDescriptor.Attributes(GetType(PropertyAttributesProviderAttribute)), PropertyAttributesProviderAttribute)
            If propertyAttributesProviderAttribute IsNot Nothing Then
                Dim propertyAttributesProvider As MethodInfo = propertyAttributesProviderAttribute.GetPropertyAttributesProvider(target)
                If propertyAttributesProvider IsNot Nothing Then
                    propertyAttributesProvider.Invoke(target, New Object() {propertyAttributes})
                End If
            End If

            Return propertyAttributes
        End Function
    End Class
End Namespace
