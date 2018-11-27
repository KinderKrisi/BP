import { Address } from "../address";
import { PatientName } from "../patientName";

export class PatientVM {
	civilRegistrationNumber: string;
	civilStatusCode: string;
	civilSubstituteNumberNational: string;
	countryIdentificationCode: string;
	countryIdentificationCodeSST: string;
	countryIdentificationText: string;
	parishDistrictCode: string;
	parishDistrictText: string;
	personGenderCode: string;
	populationDistrictCode: string;
	populationDistrictText: string;
	practitionerIdentificationCode: string;
	regionalCode: string;
	regionalName: string;
	socialDistrictCode: string;
	socialDistrictText: string;
	statusCode: string;
    
    address: Address = new Address();
    patientName: PatientName = new PatientName();
}