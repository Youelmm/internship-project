var express = require('express')
const {
    request
} = require('http')
var server = express()
var game = null

// Object representing a player
function Player(gamertag) {
    this.gamertag = gamertag

    this.setGamertag = (gamertag) => {
        this.gamertag = gamertag
    }
}

// Object representing a game
function Game(maximumPoints, owner) {
    this.hasStarted = false
    this.winner = null
    this.maximumPoints = maximumPoints
    this.owner = owner
    this.players = {}
    this.points = {}

    this.addPlayer = (player) => {
        if (this.players.hasOwnProperty(player.gamertag)) {
            return false
        }
        // To add it
        this.players[player.gamertag] = player
        this.points[player.gamertag] = 0
        return true
    }
    this.addPoints = (playerGamertag, points) => {
        this.points[playerGamertag] += points
        // To check if the player wins
        if (this.points[playerGamertag] >= this.maximumPoints) {
            this.winner = this.players[playerGamertag]
        }
    }

    // Add the owner
    this.addPlayer(owner)
}

// To create a new game
server.get("/game/new", (request, response) => {
    if (!request.query["gamertag"]) {
        response.status(400).send("No gamertag provided")
    } else if (!game) {
        var owner = new Player(request.query["gamertag"])
        var maximumPoints = request.query["maximum_points"]
        if (!request.query["maximum_points"]) {
            maximumPoints = 10
        }
        game = new Game(maximumPoints, owner)
        response.status(200).send("The game has been created")
    } else {
        response.status(400).send("A game is already in progress")
    }
})

// To join the game server
server.get("/game/join", (request, response) => {
    // To check if a game was created
    if (!game) {
        response.status(400).send("No game created")
    } else if (game.hasStarted) {
        response.status(400).send("The game has already started")
    } else if (!request.query["gamertag"]) {
        response.status(400).send("No gamertag provided")
    } else if (!game.addPlayer(new Player(request.query["gamertag"]))) {
        response.status(400).send("The gamertag is already taken by another player")
    } else {
        response.status(200).send("You joined the game")
    }
})

// To start the game
server.get("/game/start", (request, response) => {
    if (!game) {
        response.status(400).send("No game created")
    } else if (game.hasStarted) {
        response.status(400).send("The game has already started")
    } else {
        game.hasStarted = true
        response.status(200).send("The game starts")
    }
})

// To add a given count of points for a given player
server.get("/game/edit/add_points", (request, response) => {
    if (!game) {
        response.status(400).send("No game created")
    } else if (!game.hasStarted) {
        response.status(400).send("The game has not started")
    } else if (game.winner) {
        response.status(400).send("The game has finished")
    } else if (!request.query["gamertag"]) {
        response.status(400).send("No gamertag provided")
    } else if (!game.players[request.query["gamertag"]]) {
        response.status(400).send("No player with this gamertag")
    } else if (!request.query["points"]) {
        response.status(400).send("No points provided")
    } else if (!parseInt(request.query["points"])) {
        response.status(400).send("Points are not a number")
    } else {
        var points = parseInt(request.query["points"])
        game.addPoints(request.query["gamertag"], points)
        if (game.winner) {
            response.status(200).send("You win!")
        } else {
            response.status(200).send("Points added")
        }
    }
    console.log(game.points)
})

// TODO: to make it work outside loopback IP address
server.listen(8080, () => {
    // TODO: to display the IP address which the server runs on
    console.log("The game server has started")
})