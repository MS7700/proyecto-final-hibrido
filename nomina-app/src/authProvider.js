

 const authProvider = {
    login: ({ username, password }) =>  {
        
        var token = window.btoa(username + ':' + password);
        sessionStorage.setItem('token',token);
        const request = new Request("https://localhost:44340/Empleado", {
            method: 'GET',
            headers: new Headers({ 'Content-Type': 'application/json',
            "Access-Control-Allow-Headers": "Authorization",
            "Access-Control-Allow-Methods": "GET, POST, DELETE","Access-Control-Allow-Origin": "*",  'Authorization' : 'Basic ' + token }),
        });
        return fetch(request)
            .then(response => {
                if (response.status < 200 || response.status >= 300) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .then(auth => {
                localStorage.setItem('auth', JSON.stringify(auth));
            })
            .catch(() => {
                throw new Error('Usuario o contraseña incorrecta')
            });
    },
    logout: () => {
        console.log("Login out: " + sessionStorage.getItem("token"));
        return Promise.resolve();
    },
    checkError: ({ status }) => {
        if (status === 401 || status === 403) {
            localStorage.removeItem('token');
            return Promise.reject();
        }
        return Promise.resolve();
    },
    checkAuth: () => {
        console.log("Check token: " + sessionStorage.getItem("token"));
        if(sessionStorage.getItem("token")){
            const request = new Request("https://localhost:44340/Empleado", {
            method: 'GET',
            headers: new Headers({ 'Content-Type': 'application/json',"Access-Control-Allow-Headers": "Authorization",
            "Access-Control-Allow-Methods": "GET, POST, DELETE","Access-Control-Allow-Origin": "*", 'Authorization' : 'Basic ' + sessionStorage.getItem("token") }),});
            return fetch(request)
            .then(response => {
                if (response.status < 200 || response.status >= 300) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .then(auth => {
                sessionStorage.setItem('auth', JSON.stringify(auth));
            })
            .catch(() => {
                throw new Error('Usuario o contraseña incorrecta')
            });    
        }else{
            return Promise.reject();
        }
        
    },
    getPermissions: () => {
        return Promise.resolve();
    }
};

export default authProvider;