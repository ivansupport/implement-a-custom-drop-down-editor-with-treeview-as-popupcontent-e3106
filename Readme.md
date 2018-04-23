# Implement a custom drop-down editor with TreeView as PopupContent


<p>Show how to implement a custom ComboBoxEdit with TreeView as a PopupContent and use it inside DXGrid as a custom column.</p><br />
<p><strong>Update (</strong><strong>version </strong><strong>13.1</strong><strong>):</strong></p><br />
<p>We've modified this code example by using the DevExpress control version 13.1. There is no need to create custom classes to resolve this problem.</p><p>You can accomplish this task by using the Grid control in the TreeView mode as a popup content template of the LookUpEdit control. </p><p>This way is more efficient and clear than the way used in this example for old versions of DevExpress controls. </p><br />
<p>Here is a snippet of the markup file:</p>

```xaml
        <dxg:LookUpEdit Name="lookUpEdit" VerticalAlignment="Top" Width="350" Margin="50,37,67,0"
                        DisplayMember="Name">
            <dxg:LookUpEdit.PopupContentTemplate>
                <ControlTemplate>
                    <dxg:GridControl x:Name="PART_GridControl" AutoGenerateColumns="AddNew">
                        <dxg:GridControl.Columns>
                            <dxg:GridColumn FieldName="Name" Header="Employee Name" />
                            <dxg:GridColumn FieldName="Position" />
                            <dxg:GridColumn FieldName="Department" />
                        </dxg:GridControl.Columns>
                        <dxg:GridControl.View>
                            <dxg:TreeListView Name="treeListView1" AutoWidth="True"
                                  KeyFieldName="ID" ParentFieldName="ParentID"
                                  TreeDerivationMode="Selfreference"/>
                        </dxg:GridControl.View>
                    </dxg:GridControl>
                </ControlTemplate>
            </dxg:LookUpEdit.PopupContentTemplate>
        </dxg:LookUpEdit>


```



<br/>


