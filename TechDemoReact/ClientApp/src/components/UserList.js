import React, { Component } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';
import { Toolbar } from 'primereact/toolbar';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import UserService from './UserService';
import classNames from 'classnames';

export class UserList extends Component {
    userService = new UserService();

    constructor(props) {
                
        super(props);
        const emptyUser = this.userService.getEmptyUser();
        this.state = {
            users: [],
            user: emptyUser,
            submitted: false,
            selectedUsers: null,
            createUserDialog: false,             
            deleteDialog: false,
            deleteManyDialog: false
        }
    }    

    componentDidMount() {
        this.loadUsers();
    }

    async loadUsers()
    {
        var users = await this.userService.getUsers();
        this.setUsers(users);

    }

    setUsers(data) {
        console.log(data);
        this.setState({ users: data });
    }

    openEditDialog(usr) {
        
        let _user = {
            id: usr.id,
            name: usr.name,
            email: usr.email,
            dateOfBirth: usr.dateOfBirth,
            password: usr.password,
            //photo: usr.photo, error in setState. why?
            photoFile: null
        }
        console.log(_user);    
        this.setState({ user: _user })
        this.setState({ createUserDialog: true });
    }    

    openCreateDialog() {
        this.setState({ createUserDialog: true });
        this.setState({ submitted: false });
        this.setState({ user: this.userService.getEmptyUser() });
    }  

    hideDialog() {        
        this.setState({ submitted: false });
        this.setState({ createUserDialog: false });
    } 

    openDeleteDialog(usr) {
        this.setState({user : usr})
        this.setState({ deleteDialog: true })
    }

    hideDeleteDialog() {
        this.setState({ deleteDialog: false })
    }

    openDeleteManyDialog(usr) {        
        this.setState({ deleteManyDialog: true })
    }

    hideDeleteManyDialog() {
        this.setState({ deleteDialog: false })

    }


    onInputChange(e, name) {      
        
        const val = (e.target && e.target.value) || '';       
        let _user = { ...this.state.user };
        _user[`${name}`] = val;
        this.setState({ user: _user });
    } 

    onPhotoChange(e) {
        const val = e.target.files[0];        
        let _user = { ...this.state.user };
        _user['photoFile'] = val;           
        this.setState({ user: _user });
    }

    async saveUser() {        
        this.setState({ submitted: true });
        if (this.state.user.name.trim()) {
            let _users = [...this.state.users];
            let _user = { ...this.state.user };
            
            _user.photo = _user.photoFile?.name ?? null;
            
            const isNew = !(_user.id);
            if (isNew) {                  
                _users.push(_user);
                this.userService.postUser(_user);
            }
            else
            {
                const idx = this.findIndex(_user, _users);

                if (!_user.photoFile) {
                    _user.photo = _users[idx].photo;
                }

                _users[idx] = _user;
                this.userService.putUser(_user);
            }
            this.setState({ users: _users });
            this.setState({ createUserDialog: false });
            this.setState({ user: this.userService.getEmptyUser() });           
        }       
    }

    findIndex(user, users) {
        let idx = -1;
        for (let i = 0; i < users.length; i++) {
            if (users[i].id === user.id) {
                idx = i;
                break;
            }       
        }
        return idx;
    }

    deleteUser() {
        let _users = this.state.users.filter(u => u.id !== this.state.user.id);
        this.setState({ users: _users });
        this.userService.deleteUser(this.state.user);
        this.setState({ user: this.userService.getEmptyUser() });
        this.setState({ deleteDialog: false });
    }

    deleteManyUser() {       
        let _users = this.state.users.filter(u => !this.state.selectedUsers.includes(u));
        this.setState({ users: _users });
        console.log(this.state.selectedUsers);
        for (const user of this.state.selectedUsers)
        {
            console.log(user);
            this.userService.deleteUser(user);
        }
        
        this.setState({ user: this.userService.getEmptyUser() });
        this.setState({ selectedUsers: null });
        this.setState({ deleteManyDialog: false });
    }

    toolbarTemplate(){
        const selected = this.state.selectedUsers;
        return (
            <React.Fragment>
                <Button label="New" icon="pi pi-plus" className="p-button-success p-mr-2" onClick={this.openCreateDialog.bind(this)} />
                <Button label="Delete" icon="pi pi-trash" className="p-button-danger" onClick={this.openDeleteManyDialog.bind(this)} disabled={!selected || !selected.length} />
            </React.Fragment >)
    }

    renderDataTable() {

        const imageBodyTemplate = (rowData) => {
            return <img src={`./public/images/${rowData.photo}`} alt={rowData.image} className="product-image rounded-circle"
                height="40" width="40" />
        }       

        const actionBodyTemplate = (rowData) => {
            return (
                <React.Fragment>
                    <Button icon="pi pi-pencil" className="p-button-rounded p-button-success p-mr-2" onClick={() => this.openEditDialog(rowData)} />
                    <Button icon="pi pi-trash" className="p-button-rounded p-button-warning" onClick={() => this.openDeleteDialog(rowData)} />
                </React.Fragment>
            );
        }

        const createDialogFooter = () => {
            return (
                <React.Fragment>
                    <Button label="Cancel" icon="pi pi-times" className="p-button-text" onClick={this.hideDialog.bind(this)} />
                    <Button label="Save" icon="pi pi-check" className="p-button-text" onClick={this.saveUser.bind(this)} />
                </React.Fragment>)}

        const deleteDialogFooter = () => {
            return(
            <React.Fragment>
                 <Button label="Cancel" icon="pi pi-times" className="p-button-text" onClick={this.hideDeleteDialog.bind(this)} />
                 <Button label="Delete" icon="pi pi-check" className="p-button-text" onClick={this.deleteUser.bind(this)} />
                </React.Fragment>)
        }

        const deleteManyDialogFooter = () => {
            return (
                <React.Fragment>
                    <Button label="Cancel" icon="pi pi-times" className="p-button-text" onClick={this.hideDeleteManyDialog.bind(this)} />
                    <Button label="Delete" icon="pi pi-check" className="p-button-text" onClick={this.deleteManyUser.bind(this)} />
                </React.Fragment>)
        }

        return (      
        <>
            <div className="card">
                <Toolbar className="p-mb-4" left={this.toolbarTemplate()} />
                <DataTable value={this.state.users} selection={this.state.selectedUsers} onSelectionChange={(e) => this.setState({ selectedUsers: e.value })} >
                    <Column selectionMode="multiple" headerStyle={{ width: '3rem' }}></Column>
                    <Column field="name" header="Name" sortable></Column>
                    <Column type='email' field="email" header="Email"></Column>
                    <Column field="dateOfBirth" header="Date of birth"></Column>
                    <Column field="password" header="Password"></Column>
                    <Column field="photo" header="Photo" body={imageBodyTemplate}></Column>
                    <Column body={actionBodyTemplate}></Column>
                </DataTable>
        </div>
            {/*why onChange={(e) => this.onInputChange(e, 'name') but onHide={this.hideDialog.bind(this) why dufferent behaviour*/}
            <Dialog visible={this.state.createUserDialog} style={{ width: '400px' }} header='Create User Dialog' footer={createDialogFooter} modal className='p-fluid' onHide={this.hideDialog.bind(this)}>
            <div className="p-field">
                        <label htmlFor="name">Name</label>
                        <InputText id="name" value={this.state.user.name} onChange={(e) => this.onInputChange(e, 'name')} required autoFocus className={classNames({ 'p-invalid': this.submitted && !this.state.user.name })} />
                        {this.submitted && !this.state.user.name && <small className="p-error">Name is required.</small>}
            </div>
             <div className="p-field">
                        <label htmlFor="email">Email</label>
                        <InputText id="email" value={this.state.user.email} onChange={(e) => this.onInputChange(e, 'email')} required autoFocus className={classNames({ 'p-invalid': this.submitted && !this.state.user.email })} />
                        {this.submitted && !this.state.user.email && <small className="p-error">Email is required.</small>}
             </div>
             <div className="p-field">
                        <label htmlFor="password">Password</label>
                        <InputText id="password" value={this.state.user.password} onChange={(e) => this.onInputChange(e, 'password')} required autoFocus className={classNames({ 'p-invalid': this.submitted && !this.state.user.password })} />
                        {this.submitted && !this.state.user.password && <small className="p-error">Password is required.</small>}
             </div> 
              <div className="p-field">
                        <label htmlFor="dateOfBirth">Date Of Birth</label>
                        <InputText id="dateOfBirth" value={this.state.user.dateOfBirth} onChange={(e) => this.onInputChange(e, 'dateOfBirth')} required autoFocus className={classNames({ 'p-invalid': this.submitted && !this.state.user.dateOfBirth })} />
                        {this.submitted && !this.state.user.dateOfBirth && <small className="p-error">date of birth is required.</small>}
              </div>  
              <div className="p-field">
                        <label htmlFor="photo">Photo</label>
                        <InputText type="file" id="photo" value={this.state.user.photo} onChange={(e) => this.onPhotoChange(e)} required autoFocus className={classNames({ 'p-invalid': this.submitted && !this.state.user.photo })} />
                        {this.submitted && !this.state.user.photo && <small className="p-error">Photo is required.</small>}
              </div>  

                </Dialog>

                <Dialog visible={this.state.deleteDialog} style={{ width: '400px' }} header='Delete User Dialog' footer={deleteDialogFooter} modal className='p-fluid' onHide={this.hideDeleteDialog.bind(this)}>
                    <div className="confirmation-content">
                        <i className="pi pi-exclamation-triangle p-mr-3" style={{ fontSize: '2rem' }} />
                        {this.state.user && <span>Are you sure you want to delete <b>{this.state.user.name}</b>?</span>}
                    </div>
                </Dialog>

                <Dialog visible={this.state.deleteManyDialog} style={{ width: '400px' }} header='Delete User Dialog' footer={deleteManyDialogFooter} modal className='p-fluid' onHide={this.hideDeleteManyDialog.bind(this)}>
                    <div className="confirmation-content">
                        <i className="pi pi-exclamation-triangle p-mr-3" style={{ fontSize: '2rem' }} />
                        {this.state.user && <span>Are you sure you want to delete <b>{this.state.user.name}</b>?</span>}
                    </div>
                </Dialog>



        </>
    );        
    }

    render() {
        return this.renderDataTable();
    }


}
