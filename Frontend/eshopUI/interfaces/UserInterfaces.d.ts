export interface IUser {
	email: string;
	password: string,
}

export interface IUserLogin {
	email: string;
	password: string,
	remember: boolean
}