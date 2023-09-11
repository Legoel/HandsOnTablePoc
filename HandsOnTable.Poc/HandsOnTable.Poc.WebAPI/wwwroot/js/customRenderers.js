const addClassWhenNeeded = (td, cellProperties) => {
    const className = cellProperties.className;

    if (className !== void 0) {
        Handsontable.dom.addClass(td, className);
    }
};

function tooltipRenderer(
    instance,
    td,
    row,
    column,
    prop,
    value,
    cellProperties
) {
    td.title = value;
    Handsontable.renderers.TextRenderer.apply(this, [
        instance,
        td,
        row,
        column,
        prop,
        value,
        cellProperties
    ]);

}
Handsontable.renderers.registerRenderer('silae.tooltip', tooltipRenderer);

async function manufacturerRenderer(
    instance,
    td,
    row,
    column,
    prop,
    value,
    cellProperties
) {
    Handsontable.renderers.BaseRenderer.apply(this, arguments);

    // Pour appeler la liste à chaque rendu mais attention l'impact sur les performances est important
    // Car la méthode de rendu peut-être appelée plusieurs fois pour chaque cellule !
    //const manufacturerResponse = await fetch(`https://localhost:7274/manufacturers/name/${value}`)
    //const manufacturer = await manufacturerResponse.json();

    // Pour optimiser les performances on se base sur la liste des 'manufacturers' définie dans la page principale
    // Ce n'est pas très propre mais ça évite quelques centaines de requêtes HTTP...
    // La bonne manière serait probablement de redéfinir complétement un type de colonne
    // Voir la doc officielle : https://handsontable.com/docs/javascript-data-grid/cell-editor/
    const manufacturer = manufacturers.find((item) => item.name === value);

    let flag = "";
    if (manufacturer) {
        switch (manufacturer.country) {
            case Countries.France:
                flag = "🇫🇷";
                break;
            case Countries.Spain:
                flag = "🇪🇸";
                break;
            case Countries.Germany:
                flag = "🇩🇪";
                break;
            case Countries.Portugal:
                flag = "🇵🇹";
                break;
            case Countries.Belgium:
                flag = "🇧🇪";
                break;
            case Countries.Italy:
                flag = "🇮🇹";
                break;
            case Countries.Swiss:
                flag = "🇨🇭";
                break;
        };
    }

    Handsontable.renderers.TextRenderer.apply(this, [
        instance,
        td,
        row,
        column,
        prop,
        `${flag} ${manufacturer && manufacturer.name}`,
        cellProperties
    ]);
}
Handsontable.renderers.registerRenderer('silae.manufacturer', manufacturerRenderer);

function starRenderer(
    instance,
    td,
    row,
    column,
    prop,
    value,
    cellProperties
) {
    Handsontable.renderers.TextRenderer.apply(this, [
        instance,
        td,
        row,
        column,
        prop,
        "★".repeat(value),
        cellProperties
    ]);
}
Handsontable.renderers.registerRenderer('silae.star', starRenderer);

function conditionalFormatingRenderer(
    instance,
    td,
    row,
    column,
    prop,
    value,
    cellProperties
) {
    let icon = "";
    switch (true) {
        case (typeof (cellProperties.green) == 'function' && cellProperties.green(value)):
            icon = '🟢'; // pastille verte
            break;
        case (typeof (cellProperties.yellow) == 'function' && cellProperties.yellow(value)):
            icon = '🟡'; // pastille jaune
            break;
        case (typeof (cellProperties.orange) == 'function' && cellProperties.orange(value)):
            icon = '🟠'; // pastille orange
            break;
        case (typeof (cellProperties.red) == 'function' && cellProperties.red(value)):
            icon = '🔴'; // pastille rouge
            break;
    };
    const div = document.createElement("div");

    const iconContainer = document.createElement("span");
    iconContainer.innerText = icon;
    Handsontable.dom.addClass(iconContainer, "floatLeft");

    const valueContainer = document.createElement("span");
    valueContainer.innerText = value;
    Handsontable.dom.addClass(valueContainer, "floatRight");

    div.appendChild(iconContainer);
    div.appendChild(valueContainer);

    addClassWhenNeeded(td, cellProperties);
    Handsontable.dom.empty(td);

    td.appendChild(div);
}
Handsontable.renderers.registerRenderer('silae.conditional', conditionalFormatingRenderer);

function arrayMapRenderer(
    instance,
    td,
    row,
    column,
    prop,
    value,
    cellProperties
) {
    let numValue = Number(value);
    let finalValue = value;

    if (!isNaN(numValue) && Array.isArray(cellProperties.map) && cellProperties.map.length > numValue) {
        finalValue = cellProperties.map[numValue];
    }

    Handsontable.renderers.TextRenderer.apply(this, [
        instance,
        td,
        row,
        column,
        prop,
        finalValue,
        cellProperties
    ]);
}
Handsontable.renderers.registerRenderer('silae.arrayMap', arrayMapRenderer);

