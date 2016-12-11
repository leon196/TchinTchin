
var renderer, stage, background, hand;
var resources = PIXI.loader.resources;

PIXI.loader.add("images/background.jpg");
PIXI.loader.add("images/hand.png");
PIXI.loader.add("images/arm.png");
PIXI.loader.once("complete", start);
PIXI.loader.load();

function start (loader, res)
{
	renderer = PIXI.autoDetectRenderer(window.innerWidth, window.innerHeight, {antialias: false, transparent: false, resolution: 1});
	renderer.autoResize = true;
	document.body.appendChild(renderer.view);

	stage = new PIXI.Container();
	background = new PIXI.Sprite(resources["images/background.jpg"].texture);
	hand = new PIXI.Sprite(resources["images/hand.png"].texture);
	stage.addChild(background);
	stage.addChild(hand);

	mouse.setup(stage);

	update();
}

function update (time)
{
  requestAnimationFrame(update);

  time /= 1000;

  hand.x = mouse.x;
	hand.y = mouse.y;

	renderer.render(stage);
}