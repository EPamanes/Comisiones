<%@ Page Title="Viajes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelUser.aspx.cs" Inherits="Calculo_Comisiones_Operadores.TravelUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--------------------------- PANTALLA DE VIAJE ---------------------------%>
    <h1><b><%: Title %></b>
    </h1>
    <br />
    &nbsp
    <div class="tab-content" align="center">
        <div class="tab-panel">

            <div class="panel-heading form-inline" style="background-color: gray; height: 52px;">
                <div class="form-group pull-left">
                    <label for="Opcion">Buscar: &nbsp</label>
                    
                    <asp:DropDownList runat="server" ID="listStatusShearch" Font-Bold="true" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">----Status-----</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp
                    <asp:DropDownList runat="server" ID="listTravelShearch" Font-Bold="true" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">----Tipo Viaje-----</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp
                    <%--<asp:TextBox ID="txtSearch" runat="server" Width="277px"></asp:TextBox>--%>
                    <asp:ImageButton ID="btnSearch" runat="server" ControlStyle-CssClass="btn btn-primary" ImageUrl="Imagenes/search.png" OnClick="btnSearch_Click" ToolTip="Buscar"></asp:ImageButton>
                    <%--OnClick="btnSearch_Click"--%>
                </div>
                <div class="form-group pull-right">
                    <asp:ImageButton ID="btnNew" runat="server" ControlStyle-CssClass="btn btn-success" ImageUrl="Imagenes/add.png" OnClick="btnNew_Click" ToolTip="Agregar Viaje"></asp:ImageButton>
                    <%-- OnClick="btnAdd_Click"--%>
                </div>
            </div>
            <br />
            <%--<asp:Timer ID="timer" runat="server" OnTick="timer_Tick"></asp:Timer>--%>
            <asp:GridView ID="dgvTravel" runat="server" Width="100%" AllowPaging="true" OnPageIndexChanging="dgvTravel_PageIndexChanging" AutoGenerateColumns="false" DataKeyNames="scco_id" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" ForeColor="Black" CssClass="table table-hover table-striped" OnRowCommand="dgvTravel_RowCommand" HorizontalAlign="Center">
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000" Font-Bold="True" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />

                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:ButtonField DataTextField="scco_id" HeaderText=" ID" />
                    <asp:ButtonField DataTextField="scco_control_name" HeaderText=" Status" />
                    <asp:ButtonField DataTextField="scco_tipo" HeaderText=" Tipo de Viaje" />
                    <asp:ButtonField DataTextField="scco_unidad_name" HeaderText=" Unidad" />
                    <asp:ButtonField DataTextField="scco_operador_name" HeaderText=" Operador" />
                    <asp:ButtonField DataTextField="scco_date_out_str" HeaderText=" Fecha de Salida" />
                    <asp:ButtonField DataTextField="scco_kilometraje_out" HeaderText=" Kilometraje de Salida" />
                    <asp:ButtonField DataTextField="scco_date_in_str" HeaderText=" Fecha de Entrada" />
                    <asp:ButtonField DataTextField="scco_kilometraje_in" HeaderText=" Kilometraje de Entrada" />
                    <%--<asp:ButtonField DataTextField="scco_hora" HeaderText=" Hora" />--%>
                    <%--<asp:ButtonField CommandName="viewRecord" ControlStyle-CssClass="btn btn-info" ButtonType="Image" ImageUrl="Imagenes/user.png" HeaderText=" Detalle"></asp:ButtonField>--%>
                    <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-warning" ButtonType="Image" ImageUrl="Imagenes/edit.png" HeaderText=" Modificar"></asp:ButtonField>
                    <%--<asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-danger" ButtonType="Image" ImageUrl="imagenes/trash.png" HeaderText=" Eliminar"></asp:ButtonField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <%--------------------------- MODAL DE VIAJE AGREGAR---------------------------%>
    <div id="addModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="addModalLabel">Viaje - Agregar</h3>
                </div>
                <asp:UpdatePanel ID="updatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <div class="container-fluid">
                                    <asp:HiddenField ID="txtIdAdd" runat="server" />
                                    <div class="row">

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Operador:</asp:Label>
                                            <asp:DropDownList runat="server" ID="listOperadorAdd" CssClass="dropbtn" Font-Bold="true"></asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Unidad:</asp:Label>
                                            <asp:DropDownList runat="server" ID="listUnidadadd" CssClass="dropbtn" Font-Bold="true"></asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Fecha de Salida:</asp:Label>
                                            <asp:TextBox ID="txtDateOutAdd" runat="server" CssClass="col-sm-4 form-control" Style="text-align: center; width: 150px;" Font-Bold="true" TextMode="Date"></asp:TextBox>&nbsp&nbsp
                                            <asp:TextBox ID="txtHoraOutAdd" runat="server" CssClass="col-sm-4 form-control" Style="text-align: center; width: 129px;" Font-Bold="true" MaximumValue="23:59:59" MinimumValue="00:00:00"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="maskHoraAdd" runat="server" Mask="99:99:99" TargetControlID="txtHoraOutAdd" MaskType="Time" AcceptNegative="None" ErrorTooltipEnabled="true" />
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Kilometraje de Salida:</asp:Label>
                                            <asp:TextBox ID="txtKilOutAdd" runat="server" CssClass="col-sm-8 form-control" Style="text-align: center" Font-Bold="true" TextMode="Number"></asp:TextBox>
                                        </div>

                                        <br />
                                        <div class="GridviewDiv col-lg-12" align="center">
                                            <asp:GridView runat="server" ID="gvDetails" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDeleting="gvDetails_RowDeleting">
                                                <HeaderStyle CssClass="headerstyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="rowid" HeaderText="ID" ReadOnly="true" />
                                                    <asp:TemplateField HeaderText="Cliente">
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="txtName" runat="server" />--%>
                                                            <asp:DropDownList runat="server" ID="listClienteAdd" Font-Bold="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">----Selecciona-----</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Codigo Factura">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFactura" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:CommandField ShowDeleteButton="true" />
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:Button ID="btnAddDGV" ControlStyle-CssClass="btn btn-warning" Text="+" Font-Bold="true" runat="server" OnClick="btnAddDGV_Click" ToolTip="Agregar Cliente" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvTravel" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--------------------------- MODAL DE CLIENTE ELIMINAR---------------------------%>
    <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="delModalLabel">Viaje - Eliminar</h3>
                </div>
                <asp:UpdatePanel ID="deletePanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Esta seguro que desea eliminar este viaje?
                    <asp:HiddenField ID="deleteID" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-success" OnClick="btnDelete_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--------------------------- MODAL DE VIAJE ACTUALIZAR---------------------------%>
    <div id="editModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <h3 id="editModalLabel">Viaje - Actualizar</h3>
                </div>
                <asp:UpdatePanel ID="updatePanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <div class="container-fluid">
                                    <asp:HiddenField ID="txtIdMod" runat="server" />
                                    <div class="row">

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Operador:</asp:Label>
                                            <asp:DropDownList runat="server" ID="listOperadorMod" CssClass="dropbtn" Font-Bold="true" Enabled="false"></asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Unidad:</asp:Label>
                                            <asp:DropDownList runat="server" ID="listUnidadMod" CssClass="dropbtn" Font-Bold="true" Enabled="false"></asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Fecha de Salida:</asp:Label>
                                            <asp:TextBox ReadOnly="true" ID="txtDateOutMod" runat="server" CssClass="col-sm-4 form-control" Style="text-align: center; width: 150px;" Font-Bold="true" TextMode="Date"></asp:TextBox>&nbsp&nbsp
                                            <asp:TextBox ReadOnly="true" ID="txtHoraOutMod" runat="server" CssClass="col-sm-4 form-control" Style="text-align: center; width: 129px;" Font-Bold="true" MaximumValue="23:59:59" MinimumValue="00:00:00"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="maskHoraMod" runat="server" Mask="99:99:99" TargetControlID="txtHoraOutMod" MaskType="Time" AcceptNegative="None" ErrorTooltipEnabled="true" />
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Kilometraje de Salida:</asp:Label>
                                            <asp:TextBox ReadOnly="true" ID="txtKilOutMod" runat="server" CssClass="col-sm-8 form-control" Style="text-align: center" Font-Bold="true" TextMode="Number"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Fecha de Entrada:</asp:Label>
                                            <asp:TextBox ID="txtDateInMod" runat="server" CssClass="col-sm-4 form-control" Style="text-align: center; width: 150px;" Font-Bold="true" TextMode="Date"></asp:TextBox>&nbsp&nbsp
                                            <asp:TextBox ID="txtHoraInMod" runat="server" CssClass="col-sm-4 form-control" Style="text-align: center; width: 129px;" Font-Bold="true" MaximumValue="23:59:59" MinimumValue="00:00:00"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="maskHoraInMod" runat="server" Mask="99:99:99" TargetControlID="txtHoraInMod" MaskType="Time" AcceptNegative="None" ErrorTooltipEnabled="true" />
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Label runat="server" CssClass="col-sm-4 control-label" Font-Bold="true" Style="text-align: right;">Kilometraje de Entrada:</asp:Label>
                                            <asp:TextBox ID="txtKilInMod" runat="server" CssClass="col-sm-8 form-control" Style="text-align: center" Font-Bold="true" TextMode="Number"></asp:TextBox>
                                        </div>

                                        <br />
                                        <div class="GridviewDiv col-lg-12" align="center">
                                            <asp:GridView runat="server" ID="dgvDetailMod" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Enabled="false">
                                                <HeaderStyle CssClass="headerstyle" />
                                                <Columns>
                                                    <%--<asp:BoundField DataField="rowid" HeaderText="ID" ReadOnly="true" /> --%>
                                                    <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="rowid" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="  Cliente">
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="txtName" runat="server" />--%>
                                                            <asp:DropDownList runat="server" ID="listClienteMod" Font-Bold="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">----Selecciona-----</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="  Codigo Factura">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFacturaMod" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:CommandField ShowDeleteButton="true" />--%>
                                                </Columns>
                                            </asp:GridView>
                                            <%--<br />--%>
                                            <%--<asp:Button ID="Button1" ControlStyle-CssClass="btn btn-warning" Text="+" Font-Bold="true" runat="server" OnClick="btnAddDGV_Click" ToolTip="Agregar Cliente"/>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgvTravel" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>