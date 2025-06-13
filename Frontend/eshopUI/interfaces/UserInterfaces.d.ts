export interface IUser {
	username: string,
	firstname: string
	lastname: string,
	email: string;
	password: string,
	roles: []
}

export interface IUserLoginDTO {
	email: string;
	password: string,
	remember: boolean
}