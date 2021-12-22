let user = {
	name: "John Smith",
	email: "js@someFakeEmailServer.com",
	songs: [
		{
			artist: "Arctic Monkeys",
			name: "505",
		},
		{
			artist: "Three Days Grace",
			name: "Break",
		},
	],
};

let validSong = {
	artist: "Arctic Monkeys",
	name: "Do I Wann Know?",
};

function addToPlaylist(user, songToAdd) {
	let canAdd;
	for (const key of Object.keys(user)) {
		if (key == "songs") {
			for (let i = 0; i < user[key].length; i++) {
				if (user[key][i] == songToAdd) {
					canAdd = false;
				} else {
					canAdd = true;
				}
			}
			if (canAdd) {
				user[key].push(songToAdd);
			}
		}
	}
}
