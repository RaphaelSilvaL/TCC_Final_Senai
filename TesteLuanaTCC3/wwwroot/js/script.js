
//abrindo o menu dropdown da navbar
const toggleBtn = document.querySelector('.toggle_btn')
const toggleBtnIcon = document.querySelector('.toggle_btn i')
const dropDownMenu = document.querySelector('.menu_dropdown')

toggleBtn.onclick = function () {
    dropDownMenu.classList.toggle('open')
    const isOpen = dropDownMenu.classList.contains('open')

    toggleBtnIcon.classList = isOpen
        ? 'fa-solid fa-xmark'
        : 'fa-solid fa-bars'
}


//scroll suave
window.scroll({ top: 0, behavior: 'smooth' })




// slider transição automatica das imagens

let count = 1;
document.getElementById("radio1").checked = true;

setInterval( function(){
nextImage();
},3000)

function nextImage (){
    count++;
    if(count>4){
        count=1;
    }

    document.getElementById("radio"+count).checked = true;
}




                    // modal filtros

// Obter o modal
const modal = document.querySelector('.modal');

// Obter o botão que abre o modal
const btnAbrirModal = document.querySelector('.btn-abrir-modal');

// Obter o botão de fechar
const btnFecharModal = document.querySelector('.close');

// Abrir o modal quando o botão é clicado
btnAbrirModal.addEventListener('click', function () {
  modal.style.display = 'block';
});

// Fechar o modal quando o botão de fechar é clicado
btnFecharModal.addEventListener('click', () => {
  modal.style.display = 'none';
});

// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
  if (event.target == modal) {
    modal.style.display = 'none';
  }
});





                    // modal login

// Obter o modal
const modalLogin = document.querySelector('.modalLogin');

// Obter o botão que abre o modal
const abrirModalLogin = document.querySelector('.abrir-login');

// Obter o botão de fechar
const fecharModalLogin = document.querySelector('.closeLogin');

// Abrir o modal quando o botão é clicado
abrirModalLogin.addEventListener('click', function() {
  modalLogin.style.display = 'block';
});

var openModalBtn = document.getElementById("modal-login");

function abrir() {
    let modalLogin = document.querySelector('.modalLogin');
    modalLogin.style.display = 'block';
}


// Fechar o modal quando o botão de fechar é clicado
fecharModalLogin.addEventListener('click', function () {
  modalLogin.style.display = 'none';
});

// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
  if (event.target == modalLogin) {
    modalLogin.style.display = 'none';
  }
});



                    // modal recuperar senha

// Obter o modal
const modalRecuperar = document.querySelector('.modalRecuperar');


// Obter o botão que abre o modal
const abrirModalRecuperar = document.querySelector('.abrir-recuperar');

// Obter o botão de fechar
const fecharModalRecuperar = document.querySelector('.closeRecuperar');

// Abrir o modal quando o botão é clicado
abrirModalRecuperar.addEventListener('click', () => {
  modalRecuperar.style.display = 'block';
  modalLogin.style.display = 'none';
});

// Fechar o modal quando o botão de fechar é clicado
fecharModalRecuperar.addEventListener('click', () => {
  modalRecuperar.style.display = 'none';

});

// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
  if (event.target == modalRecuperar) {
    modalRecuperar.style.display = 'none';
  }
});



                        // modal cadastrar-se

// Obter o modal
const modalCadastrar = document.querySelector('.modalCadastrar');


// Obter o botão que abre o modal
const abrirModalCadastrar = document.querySelector('.abrir-cadastrar');

// Obter o botão de fechar
const fecharModalCadastrar = document.querySelector('.closeCadastrar');

// Abrir o modal quando o botão é clicado
abrirModalCadastrar.addEventListener('click', () => {
  modalCadastrar.style.display = 'block';

});

// Fechar o modal quando o botão de fechar é clicado
fecharModalCadastrar.addEventListener('click', () => {
  modalCadastrar.style.display = 'none';

});

// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
  if (event.target == modalCadastrar) {
    modalCadastrar.style.display = 'none';
  }
});


                // modal editar perfil

// Obter o modal
const modalEditarPerfil = document.querySelector('.modalEditarPerfil');


// Obter o botão que abre o modal
const abrirModalEditarPerfil = document.querySelector('.abrir-editar-perfil');

// Obter o botão de fechar
const fecharModalEditarPerfil = document.querySelector('.closeEditarPerfil');

// Abrir o modal quando o botão é clicado
abrirModalEditarPerfil.addEventListener('click', () => {
  modalEditarPerfil.style.display = 'block';

});

// Fechar o modal quando o botão de fechar é clicado
fecharModalEditarPerfil.addEventListener('click', () => {
  modalEditarPerfil.style.display = 'none';

});

// Fechar o modal quando o usuário clicar fora da caixa do modal
window.addEventListener('click', (event) => {
  if (event.target == modalEditarPerfil) {
    modalEditarPerfil.style.display = 'none';
  }
});

