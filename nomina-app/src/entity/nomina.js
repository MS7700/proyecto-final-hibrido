import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create,  SimpleForm, 
    NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput,DateField,
    DateInput, FormDataConsumer,RadioButtonGroupInput,Toolbar,SaveButton   } from 'react-admin';


export const NominaList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Descripcion" />
        </Datagrid>
    </List>
);

export const NominaEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="Descripcion" />
        </SimpleForm>
    </Edit>
);

export const NominaCreate = props => (
    <Create {...props}>
        <SimpleForm>
        <DateInput source="Fecha" />
        
            <ReferenceInput label="Tipo Nomina" source="TipoNominaID" reference="TipoNomina" >
                <SelectInput label="Tipo Nomina"  optionText="Descripcion" />
            </ReferenceInput>
        </SimpleForm>
    </Create>
);