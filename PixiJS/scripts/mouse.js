
var mouse = {};

mouse.x = 0;
mouse.y = 0;
mouse.down = false;
mouse.clic = false;

mouse.drag = new vec2();
mouse.dragOrigin = new vec2();
mouse.dragLast = new vec2();
mouse.dragDelta = new vec2();

mouse.wheel = 0;
mouse.wheelDelta = 0;

mouse.start = 0;
mouse.delay = 0.5;

mouse.update = function ()
{
	if (mouse.down) {
		mouse.drag.x = mouse.x - mouse.dragOrigin.x;
		mouse.drag.y = mouse.y - mouse.dragOrigin.y;
		mouse.dragDelta.x = mouse.x - mouse.dragLast.x;
		mouse.dragDelta.y = mouse.y - mouse.dragLast.y;
	}
	mouse.dragLast.x = mouse.x;
	mouse.dragLast.y = mouse.y;
};

mouse.mouseMove = function(event)
{
	mouse.x = event.data.global.x;
	mouse.y = event.data.global.y;
};

mouse.mouseDown = function(event)
{
	mouse.x = event.data.global.x;
	mouse.y = event.data.global.y;
	mouse.dragOrigin.x = mouse.x;
	mouse.dragOrigin.y = mouse.y;
	mouse.down = true;
	mouse.clic = true;
};

mouse.mouseUp = function(event)
{
	mouse.down = false;
	mouse.clic = false;
};

mouse.mouseWheel = function(x, y)
{
	y = y > 0 ? 100 : -100;
	mouse.wheel += y;
	mouse.wheelDelta = y;	
};

mouse.setup = function (container)
{
	container.interactive = true;
	container.on('mousedown', mouse.mouseDown).on('touchstart', mouse.mouseDown);
	container.on('touchend', mouse.mouseUp);
	window.onmouseup = mouse.mouseUp;
	// container.on('mouseout', mouse.mouseUp);
	container.on('mousemove', mouse.mouseMove).on('touchmove', mouse.mouseMove);
	
	// addWheelListener(renderer.view, function (e) {
	// 	mouse.mouseWheel(e.deltaX, e.deltaY);
	// });
};