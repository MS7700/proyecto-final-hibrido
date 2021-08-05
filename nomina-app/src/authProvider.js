

 const authProvider = {
    login: ({ username, password }) =>  {
        
        var token = window.btoa(username + ':' + password);
        sessionStorage.setItem('token',token);
        console.log("Login in: " + sessionStorage.getItem("token"));
        const request = new Request("https://localhost:44340/Autenticar", {
            method: 'GET',
            headers: new Headers({ 'Content-Type': 'application/json',
            "Access-Control-Allow-Headers": "Authorization",
            "Access-Control-Allow-Methods": "GET, POST, DELETE","Access-Control-Allow-Origin": "*",  'Authorization' : 'Basic ' + token }),
        });
        return fetch(request)
            .then(response => {
                
                if (response.status > 200 && response.status < 401) {
                    throw new Error(response.statusText);
                }
                if(response.status == 401){
                    throw new Error('Usuario o contraseña incorrecta');
                }
                response.json().then((identity)=>{
                    sessionStorage.setItem('id',identity['id']);
                    sessionStorage.setItem('Usuario',identity['Usuario']);
                    sessionStorage.setItem('Roles',identity['Roles']);
                    sessionStorage.setItem('Email',identity['Email']);
                    sessionStorage.setItem('Nombre',identity['Nombre']);
                    sessionStorage.setItem('Apellido',identity['Apellido']);
                });
                
                return response;
            })
            .then(() => {
                
                sessionStorage.setItem('auth', 'true');
                return Promise.resolve();
            })
            .catch((error) => {
                console.error(error);
                throw new Error(error);
            });
    },
    logout: () => {
        sessionStorage.removeItem('auth');
        sessionStorage.removeItem('token');
        return Promise.resolve();
    },
    checkError: ({ status }) => {
        if (status === 401 || status === 403) {
            sessionStorage.removeItem('token');
            return Promise.reject();
        }
        return Promise.resolve();
    },
    getIdentity:() =>{

        const id = sessionStorage.getItem('id');
        const fullName = sessionStorage.getItem('Nombre') + " " + sessionStorage.getItem('Apellido');
        const avatar = 'https://cdn1.vectorstock.com/i/1000x1000/89/50/generic-person-gray-photo-placeholder-man-vector-24848950.jpg';
        return Promise.resolve({ id, fullName,avatar });
    },
    checkAuth: () => {
        console.log(sessionStorage.getItem('auth') === 'true');
        return sessionStorage.getItem('auth') === 'true'
        ? Promise.resolve()
        : Promise.reject()

    },
    getPermissions: () => {
        const roles = sessionStorage.getItem('Roles').split(",");
        return Promise.resolve(roles);
    }
};

export default authProvider;