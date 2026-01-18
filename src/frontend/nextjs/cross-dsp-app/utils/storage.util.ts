export interface AppStorage {
    getItemByKey<T>(key: string) : StorageResult<T>;
    setItemByKey<T>(key: string, data: T): void; 
}

export type StorageResult<T> = {
    data?: T
    isSuccess: boolean;
}

export class LocalStorage implements AppStorage {
    constructor() {

    }

    getItemByKey<T>(key: string): StorageResult<T> {
        let result : StorageResult<T> = {
            isSuccess: false
        }

        const stringData = localStorage.getItem(key)
        if (stringData === null || stringData === undefined) {
            return result;
        }

        const data = JSON.parse(stringData) as T;
        result.data = data
        result.isSuccess = true

        return result;
    }

    setItemByKey<T>(key: string, data: T): void {
        localStorage.setItem(key, JSON.stringify(data));
    }
}