# ShoppingCart Exercise

## Project Brief

### Step 1: Shopping cart
*	You are building a checkout system for a shop which only sells apples and oranges.
*	Apples cost 60p and oranges cost 25p.
*	Build a checkout system which takes a list of items scanned at the till and outputs the total cost
	*	For example: [ Apple, Apple, Orange, Apple ] => £2.05
*	Make reasonable assumptions about the inputs to your solution; for example, many candidates take a list of strings as input

### Step 2: Simple offers
*	The shop decides to introduce two new offers
	*	buy one, get one free on Apples
	*	3 for the price of 2 on Oranges
*	Update your checkout functions accordingly

## Comments

The project was conceived from the point of view that additional requirements may appear at a later time.  It also seemed prudent to ensure that the cart processing was system agnostic.  Therefore a REST service using .Net Core was used to ensure it could be deployed to any platform.  Its design was such that new functionality could be added easily, but without interfering with existing functionality.

The 'Till' system was also designed to be platform agnostic and therefore used a .Net Core console service to gather user input.

After discussions with the business it was decided that we should form an online presence to capture more of the market.  To that end a simple web application was designed and implemented using AngularJS.  This web application also takes advantage of the Cart Service API so that functionality is the same across all channels.