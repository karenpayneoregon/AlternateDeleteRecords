Public Class CustomerStatements
    ''' <summary>
    ''' Select customers standard statement
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectStandard() As String

        Return <SQL>
        SELECT  C.CustomerIdentifier,
                C.CompanyName ,
                ContactType.ContactTitle ,
                CT.FirstName + ' ' + CT.LastName AS ContactNameFullName,
                C.Street ,
                C.City ,
                C.PostalCode ,
                C.CountryIdentfier ,
                C.Phone ,
                C.ModifiedDate ,
                C.InUse ,
                CT.InUse AS ContactInUse ,
                Countries.CountryName ,
                C.ContactIdentifier ,
                Countries.id AS CountryIdentifier
        FROM    Customers AS C
                INNER JOIN Contact AS CT ON C.ContactIdentifier = CT.ContactIdentifier
                INNER JOIN ContactType ON C.ContactTypeIdentifier = ContactType.ContactTypeIdentifier
                INNER JOIN Countries ON C.CountryIdentfier = Countries.id
        WHERE   ( C.InUse = @InUse  AND CT.InUse = @InUse );
               </SQL>.Value

    End Function
    ''' <summary>
    ''' Define how columns are setup for a DataGridView
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' A future TechNet article from Karen Payne will introduce how to do this
    ''' in the database which will take care of everything rather than a developer
    ''' having to perform this in their database.
    ''' </remarks>
    Public Function ConfigureDataGridViewColumns() As List(Of DataGridViewColumnDefinition)
        Return New List(Of DataGridViewColumnDefinition) From
            {
                New DataGridViewColumnDefinition() With {.Name = "CustomerIdentifier", .DisplayText = "Id", .Position = 0, .Visible = False},
                New DataGridViewColumnDefinition() With {.Name = "CompanyName", .DisplayText = "Name", .Position = 1, .Visible = True},
                New DataGridViewColumnDefinition() With {.Name = "ContactTitle", .DisplayText = "Title", .Position = 2, .Visible = True},
                New DataGridViewColumnDefinition() With {.Name = "ContactNameFullName", .DisplayText = "Contact", .Position = 3, .Visible = True},
                New DataGridViewColumnDefinition() With {.Name = "Street", .DisplayText = "Street", .Position = 8, .Visible = True},
                New DataGridViewColumnDefinition() With {.Name = "City", .DisplayText = "City", .Position = 5, .Visible = True},
                New DataGridViewColumnDefinition() With {.Name = "PostalCode", .DisplayText = "Postal", .Position = 6, .Visible = True},
                New DataGridViewColumnDefinition() With {.Name = "CountryIdentfier", .DisplayText = "Country Identfier", .Position = 7, .Visible = False},
                New DataGridViewColumnDefinition() With {.Name = "Phone", .DisplayText = "Telephone", .Position = 4, .Visible = True},
                New DataGridViewColumnDefinition() With {.Name = "ModifiedDate", .DisplayText = "Modified", .Position = 9, .Visible = False},
                New DataGridViewColumnDefinition() With {.Name = "InUse", .DisplayText = "Active", .Position = 10, .Visible = False},
                New DataGridViewColumnDefinition() With {.Name = "ContactInUse", .DisplayText = "Active", .Position = 11, .Visible = False},
                New DataGridViewColumnDefinition() With {.Name = "CountryName", .DisplayText = "Country", .Position = 12, .Visible = False},
                New DataGridViewColumnDefinition() With {.Name = "ContactIdentifier", .DisplayText = "Contact Id", .Position = 13, .Visible = False},
                New DataGridViewColumnDefinition() With {.Name = "CountryIdentifier", .DisplayText = "Country Id", .Position = 14, .Visible = False}
            }
    End Function
End Class
