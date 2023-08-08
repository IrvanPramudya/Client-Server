$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon?limit=100&offset=0"
}).done((pokemon) => {
    /*console.log(pokemon)*/
    let temporary = "";
    $.each(pokemon.results, (key, val) => {
        temporary += `<div class="col-2 me-4 my-2">
                        <div class="card bg-tortilla" style="width: 12rem;" onclick="detailPokemon('${val.url}')" 
                                data-bs-toggle="modal" data-bs-target="#modalSW" >
                        <img class="rounded-start card-img-top seperempat mx-auto" src="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/${key + 1}.png">
                            <div class="card-body mx-auto">
                                <h5 class="card-title uppercase">${val.name}</h5>
                            </div>
                        </div>
                      </div>
                      `
        /*temporary += `<tr>
                        <td>${key + 1}</td>
                        <td class="capitalize">${val.name}</td>
                        <td><button onclick="detailPokemon('${val.url}')" 
                        data-bs-toggle="modal" data-bs-target="#modalSW" 
                        class="btn btn-primary">Detail</button></td>
                    </tr>
                    `*/
    });
    $("#tbody-pokemon").html(temporary);
    $.each(pokemon.sp)
});
function detailPokemon(stringUrl) {
    let stat = ""
    let type = ""
    let ability = ""
    let item = ""
    $.ajax({
        url: stringUrl,
    }).done((pokemon) => {
        let img         = `<img src="${pokemon.sprites.other['official-artwork'].front_default}" class="img-fluid rounded-start maxcontent">`
        let imgshiny    = `<img src="${pokemon.sprites.other['official-artwork'].front_shiny}" class="img-fluid rounded-start maxcontent">`
        let nama        = `<strong class="uppercase">${pokemon.name}</strong>`
        let title       = `<marquee behavior="alternate" direction="left" class="capitalize">Detail of ${pokemon.name}</marquee>`
        let weight      = `<span class="badge rounded-pill text-dark bg-info ms-2 capitalize">${pokemon.weight} KG </span>`
        let height      = `<span class="badge rounded-pill text-dark bg-info ms-2 capitalize">${pokemon.height} M </span>`
        let baseExp     = `<div class="progress" role="progressbar" aria-label="Animated striped example" aria-valuenow="${pokemon.base_experience}" aria-valuemin="0" aria-valuemax="200">
                                <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: ${pokemon.base_experience}%">${pokemon.base_experience}</div>
                           </div>`
        $.each(pokemon.types, (key, val) => {
            if (val.type.name === "fighting" || val.type.name === "ground" || val.type.name === "rock" || val.type.name === "electric") {
                type += `<span class="badge rounded-pill text-dark bg-warning ms-2 capitalize">${val.type.name} </span>`
            }
            if (val.type.name === "bug" || val.type.name === "grass" || val.type.name === "steel") {
                type += `<span class="badge rounded-pill bg-success ms-2 capitalize">${val.type.name} </span>`
            }
            if (val.type.name === "dragon" || val.type.name === "ice") {
                type += `<span class="badge rounded-pill text-dark bg-info ms-2 capitalize">${val.type.name} </span>`
            }
            if (val.type.name === "water") {
                type += `<span class="badge rounded-pill bg-primary ms-2 capitalize">${val.type.name} </span>`
            }
            if (val.type.name === "ghost" || val.type.name === "poison" || val.type.name === "flying") {
                type += `<span class="badge rounded-pill bg-secondary ms-2 capitalize">${val.type.name} </span>`
            }
            if (val.type.name === "dark") {
                type += `<span class="badge rounded-pill bg-dark ms-2 capitalize">${val.type.name} </span>`
            }
            if (val.type.name === "fire" || val.type.name === "fairy" || val.type.name === "psychic") {
                type += `<span class="badge rounded-pill bg-danger ms-2 capitalize">${val.type.name} </span>`
            }
            if (val.type.name === "normal") {
                type += `<span class="badge rounded-pill text-dark bg-light ms-2 capitalize">${val.type.name} </span>`
            }
        })
        $.each(pokemon.abilities, (key, val) => {
            ability += `<span class="badge rounded-pill text-dark bg-info ms-2 capitalize">${val.ability.name} </span>`
        })
        $.each(pokemon.held_items, (key, val) => {
            item += `<span class="badge rounded-pill bg-dark ms-2 capitalize">${val.item.name} </span>`
        })
        $.each(pokemon.stats, (key, val) => {
            /*console.log(val.stat.name)*/
            if (val.stat.name === "hp") {
                stat += `<div class="progress mt-3" role="progressbar" aria-label="Primary example"  aria-valuemin="0" aria-valuemax="200">
                            <div class="progress-bar bg-primary align-left uppercase" style="width: ${val.base_stat}%">HP = ${val.base_stat}</div>
                        </div>`
            }
            if (val.stat.name === "attack") {
                stat += `<div class="progress mt-3" role="progressbar" aria-label="Danger example"  aria-valuemin="0" aria-valuemax="200">
                            <div class="progress-bar bg-danger align-left uppercase" style="width: ${val.base_stat}%">ATK = ${val.base_stat}</div>
                        </div>`
            }
            if (val.stat.name === "defense") {
                stat += `<div class="progress mt-3" role="progressbar" aria-label="Success example"  aria-valuemin="0" aria-valuemax="200">
                            <div class="progress-bar bg-success align-left uppercase" style="width: ${val.base_stat}%">DEF = ${val.base_stat}</div>
                        </div>`
            }
            if (val.stat.name === "special-attack") {
                stat += `<div class="progress mt-3" role="progressbar" aria-label="Dark example"  aria-valuemin="0" aria-valuemax="200">
                            <div class="progress-bar bg-dark align-left uppercase" style="width: ${val.base_stat}%;">SP-ATK = ${val.base_stat}</div>
                        </div>`
            }
            if (val.stat.name === "special-defense") {
                stat += `<div class="progress mt-3" role="progressbar" aria-label="Info example"  aria-valuemin="0" aria-valuemax="200">
                            <div class="progress-bar bg-info align-left uppercase" style="width: ${val.base_stat}%">SP-DEF = ${val.base_stat}</div>
                        </div>`
            }
            if (val.stat.name === "speed") {
                stat += `<div class="progress mt-3" role="progressbar" aria-label="Warning example"  aria-valuemin="0" aria-valuemax="200">
                            <div class="progress-bar bg-warning align-left uppercase" style="width: ${val.base_stat}%">SPD = ${val.base_stat}</div>
                        </div>`
            }

        })
        $("#pokemon-photo").html(img)
        $("#pokemonshiny-photo").html(imgshiny)
        $("#pokemon-name").html(nama)
        $("#pokemon-title").html(title)
        $("#pokemon-stat").html(stat)
        $("#pokemon-type").html(type)
        $("#pokemon-ability").html(ability)
        $("#pokemon-item").html(item)
        $("#pokemon-weight").html(weight)
        $("#pokemon-height").html(height)
        $("#pokemon-base-exp").html(baseExp)

    });

}