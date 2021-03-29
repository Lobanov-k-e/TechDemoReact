export default class UserService {

    async getUsers() {
        const responce = await fetch("User");
        const data = await responce.json();
        return data; 
    }

    async postUser(user) {
        const formData = new FormData();
        user.id = user.id ?? 0;        
        for (const name in user) {            
            formData.append(name, user[name]);
        }             
        let responce = await fetch("User", {
            method: 'POST',
            headers: {
                contentType: false,
                processData: false,
            },
            body: formData
        })    
    }

    async deleteUser(user) {
        let responce = await fetch("User", {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(user.id)
        });        
    }

    async putUser(user) {
        const formData = new FormData();        
        for (const name in user) {
            formData.append(name, user[name]);
        }
        let responce = await fetch("User", {
            method: 'PUT',
            headers: {
                contentType: false,
                processData: false,
            },
            body: formData
        })    
    }

    getEmptyUser() {
        return {
            id: null,
            name: '',
            email: '',
            dateOfBirth: null,
            password: '',  
            photo: null,
            photoFile: null
        }
    }
}


