var workflowHub = $.connection.workflowHub;

$.connection.hub.start().done(function () {
	$("#btn-notify").click(function () {
		workflowHub.server.notify();
	});
});

workflowHub.client.receiveNotification = function () {
	alert("You are notified!");
};

workflowHub.client.increaseTodoCount = function () {
	var previousCount = parseInt($("#todo-count").text());
	console.log(previousCount);
	$("#todo-count").text(previousCount + 1);
	$("#btn-notify").removeClass("btn-danger").addClass("btn-success");
};