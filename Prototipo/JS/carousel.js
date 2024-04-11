const controls = document.querySelectorAll('.control'); 
let currentContent = 0;

const sectionContent = document.querySelectorAll('.section-content');

const maxContent = sectionContent.length;

controls.forEach(control => {
    control.addEventListener('click', () =>{
        const isLeft = control.classList.contains('arrow-left')
        if(isLeft){
            currentContent -= 1;
           
        }
        else{
            currentContent += 1;
            
        }

        if(currentContent > maxContent){
            currentContent = 0;
            
        }
        
        if(currentContent < 0){
            currentContent = maxContent - 1;
            
        }

        sectionContent.forEach(content => content.classList.remove('.current-content'));

        sectionContent[currentContent].scrollIntoView({
            inline: "center",
            behavior: "smooth",
        })

        sectionContent[currentContent].classList.add("section-current");


    })
})
