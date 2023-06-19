

        //modal de login
function abrirLogin() {
    let modalLogin = document.querySelector('.modalLogin');
    modalLogin.style.display = 'block';
}

function fecharLogin() {
    let modalLogin = document.querySelector('.modalLogin');
    modalLogin.style.display = 'none';
}

    //modal recuperar senha
function abrirRecuperar() {
    let modalRecuperar = document.querySelector('.modalRecuperar');
    modalRecuperar.style.display = 'block';
}
function fecharRecuperar() {
    let modalRecuperar = document.querySelector('.modalRecuperar');
    modalRecuperar.style.display = 'none';
}
// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
    if (event.target == modalRecuperar) {
        modalRecuperar.style.display = 'none';
    }
});


        //modal cadastre-se

function abrirCadastrar() {
    let modalCadastrar = document.querySelector('.modalCadastrar');
    modalCadastrar.style.display = 'block';

}

function fecharCadastrar() {
    let modalCadastrar = document.querySelector('.modalCadastrar');
    modalCadastrar.style.display = 'none';
}

// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
    if (event.target == modalCadastrar) {
        modalCadastrar.style.display = 'none';
    }
});




       //modal de filtros
function abrirFiltros() {
    let modal = document.querySelector('.modal');
    modal.style.display = 'block';
}

function fecharFiltros() {
    let modal = document.querySelector('.modal');
    modal.style.display = 'none';
}
// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
    if (event.target == modal) {
        modal.style.display = 'none';
    }
});















    