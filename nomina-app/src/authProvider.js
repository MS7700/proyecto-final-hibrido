/*
export default {
    login: ({ username, password }) => {
        localStorage.setItem('username', username);
        localStorage.setItem('password', password);
        return Promise.resolve();
    },
   logout: () => {
        localStorage.removeItem('username');
        return Promise.resolve();
    },
    checkError: ({ status }) => {
        if (status === 401 || status === 403) {
            localStorage.removeItem('username');
            return Promise.reject();
        }
        return Promise.resolve();
    },
    checkAuth: () => {
        return localStorage.getItem('username')
            ? Promise.resolve()
            : Promise.reject();
    },
    getPermissions: () => Promise.resolve(),
 };

 */

 const authProvider = {
    login: ({ username, password }) =>  {
        console.log("Usuario:" + username);
        console.log("Contrasena:" + password);
        var token = window.btoa(username + ':' + password);
        console.log("Header:" + JSON.stringify({ 'Content-Type': 'application/json', 'Authorization' : 'Basic ' + window.btoa(username + ':' + password) }));
        sessionStorage.setItem('username', username);
        sessionStorage.setItem('password', password);
        sessionStorage.setItem('token',token);
        const request = new Request("https://localhost:44340/Empleado", {
            method: 'GET',
            headers: new Headers({ 'Content-Type': 'application/json', 'Authorization' : 'Basic ' + token }),
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
                throw new Error('Network error')
            });
           /*
            const request = new XMLHttpRequest();
            request.open('GET', "https://localhost:44340/", false, username,password)
            request.onreadystatechange = function() {
                    // D some business logics here if you receive return
               if(request.readyState === 4 && request.status === 200) {
                   console.log(request.responseText);
               }
            }
            return request.send()
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
                throw new Error('Network error')
            });
            */
    },
    logout: () => {
        console.log("Login out: " + localStorage.getItem("token"));
        sessionStorage.removeItem('token');
        return Promise.resolve();
    },
    checkError: ({ status }) => {
        if (status === 401 || status === 403) {
            //localStorage.removeItem('token');
            console.log("Error: " + localStorage.getItem("token"));
            return Promise.reject();
        }
        return Promise.resolve();
    },
    checkAuth: () => {
        console.log("Check token: " + localStorage.getItem("token"));
        if(sessionStorage.getItem("token")){
            const request = new Request("https://localhost:44340/Empleado", {
            method: 'GET',
            headers: new Headers({ 'Content-Type': 'application/json', 'Authorization' : 'Basic ' + localStorage.getItem("token") }),});
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
                throw new Error('Network error')
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