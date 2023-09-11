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

function manufacturerRenderer(
    instance,
    td,
    row,
    column,
    prop,
    value,
    cellProperties
) {
    Handsontable.renderers.BaseRenderer.apply(this, arguments);

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

