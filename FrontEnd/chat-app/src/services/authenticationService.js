export const  authService = {
    login,
    logout,
    signup
}
function signup(Email, Password, UserName) {
    console.log('servoce ' + Email, Password, UserName)
    const requestOptions = {
        method: 'POST',
        headers: {'Content-Type' : 'application/json', 'Accept' : 'application/json'},
        mode: 'cors',
        body: JSON.stringify({Email, Password, UserName})
    };
    
    return fetch(`https://localhost:5001/api/authentication/register`, requestOptions)
        .then((response) => {
            console.log(response.status)
            return response.status;
          })
        
}
function login(Email, Password) {
    const requestOptions = {
        method: 'POST',
        headers: {'Content-Type' : 'application/json', 'Accept' : 'application/json'},
        mode: 'cors',
        body: JSON.stringify({Email, Password})
    };

    return fetch(`https://localhost:5001/api/authentication/token`, requestOptions)
        .then(handleResponse)
        .then(token => {
            if(token) {
                localStorage.setItem('token',token);
            }
        });
}

function logout() {
    localStorage.removeItem('user');
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);

        if(!response.ok) {
            if(response.status === 401) {
                alert("Wrong username or Password");
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}