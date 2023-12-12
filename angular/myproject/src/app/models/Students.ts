export class showStudent {
    studentID?: number;
    studentName = "";
    studentAge!: number;
    studentEmail = "";
    createdDetails = ""; // Assuming 'CreatedDetails' can be a string or a Date
    lastModifiedDate: Date | null = null; 
    lastModifiedBy = "";
    isActive: boolean = false; // Default value can be changed based on your requirements
}
