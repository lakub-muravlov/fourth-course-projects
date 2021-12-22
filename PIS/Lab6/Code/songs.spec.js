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

let invalidSong = {
	artist: "Arctic Monkeys",
	name: "505",
};

// not refactored

// function addToPlaylist(user, songToAdd) {
// 	let canAdd;
// 	for (const key of Object.keys(user)) {
// 		if (key == "songs") {
// 			for (let i = 0; i < user[key].length; i++) {
// 				if (user[key][i] == songToAdd) {
// 					canAdd = false;
// 					return false;
// 				} else {
// 					canAdd = true;
// 				}
// 			}
// 			if (canAdd) {
// 				user[key].push(songToAdd);
// 				return user.songs;
// 			}
// 		}
// 	}
// }

//refactored
function addToPlaylist(userInstance, songToAdd) {
	if (JSON.stringify(user.songs).includes(JSON.stringify(songToAdd))) {
		return false;
	} else {
		userInstance.songs.push(songToAdd);
		return userInstance.songs;
	}
}
test("User already has this song", () => {
	expect(addToPlaylist(user, invalidSong)).toBeFalsy;
});

test("User doesn't have this song", () => {
	expect(addToPlaylist(user, validSong)).toContain(validSong);
});
