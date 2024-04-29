/* Identifica o scroll do mouse e aplica a classe CSS responsavel por tornar o objeto opaco */ 
window.onscroll = () => transparentHeader()

const header = document.getElementById("header"); 

transparentHeader = () => {
    if(window.scrollY > header.scrollTop ){
        header.classList.add("scrollTop");
    } else {
        header.classList.remove("scrollTop")
    }
}

/* seleciona todas as box e armazena em um array[] */
sciences = [... document.querySelectorAll(".box")]

/* seleciona todas as courses e armazena em um array[] */
courses = [...document.querySelectorAll(".courses")]

/*A Função setCourse() recebe um box como parametro, testa o id do box
remove a classe currentScience de todos os elementos de courses[] e depois
adiciona a mesma classe ao elemento box  */

let setCourse = (box) =>{
    switch (box.id) {
        case 'exacts': {
            courses.map(x => x.classList.remove("currentScience"))
            const science = document.querySelector(".exacts")
            science.classList.add("currentScience")
            break;
        }
        case 'humans': {
            courses.map(x => x.classList.remove("currentScience"))
            const science = document.querySelector(".humans")
            science.classList.add("currentScience")
            break;
        }
        case 'biologicals': {
            courses.map(x => x.classList.remove("currentScience"))
            const science = document.querySelector(".biologicals")
            science.classList.add("currentScience")
            break;
        }
        case 'techs': {
            courses.map(x => x.classList.remove("currentScience"))
            const science = document.querySelector(".techs")
            science.classList.add("currentScience")
            break;
        }
        default : {
            console.log(box.id)
        }
    }
}

/* Mapeia todos as boxes, adiciona um evento ao clicar, disparando a função
setCourse e passando o box como parametro */ 

sciences.map(science => {
    science.addEventListener("click", () => {setCourse(science)})
})



