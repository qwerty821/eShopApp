 
declare global {
	interface IProduct {
		id: number,
		name: string,
		price: number,
		availableStock: number,
		description: string,
		image: string,
		categoryId: number,
		slug: string,
		discount: number,
		rating: number,
		images?: string[],  
		attributes?: Record<string, string>  
	}

	interface IUserLoginDTO {
		username: string,
		password: string
	}

	interface ProductDTO {
		id: string,
		name: string,
		price: number,
		image: string,
		slug: string,
		discount: number,
		rating: number
	}
	
	interface PaginatedResultDTO<T> {
		items: T[],
		page: number,
		pageSize: Number,
		totalItems: number,
		totalPages: number
	}

}

export {};


/*
"id": 5,
"name": "iPad Air",
"price": 599,
"description": "Lightweight tablet with Apple M1 chip and 10.9-inch display",
"image": "images/ipad-air.jpg",
"availableStock": 12,
"categoryId": 3,
"slug": "ipad-air"
*/