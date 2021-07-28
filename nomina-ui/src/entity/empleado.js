import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput   } from 'react-admin';

const postFilters = [
    <TextInput label="Cedula" source="Cedula" />,
    <TextInput label="Nombre"  source="Nombre" />,
    <NumberInput label="Salario"  source="Salario" />,
    <ReferenceInput  source="DepartamentoID" reference="Departamento"><SelectInput label="Departamento"  optionText="Descripcion" /></ReferenceInput>,
    <ReferenceInput source="PuestoID" reference="Puesto"><SelectInput label="Puesto"  optionText="Descripcion" /></ReferenceInput>,
    <ReferenceInput source="NominaID" reference="Nomina"><SelectInput label="Nomina"  optionText="Descripcion" /></ReferenceInput>
];

export const EmpleadoList = props => (
    <List {...props} filters={postFilters}> 
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Cedula" />
            <TextField source="Nombre" />
            <NumberField source="Salario" />
            <ReferenceField source="DepartamentoID" reference="Departamento"><TextField source="Descripcion" /></ReferenceField>
            <ReferenceField source="PuestoID" reference="Puesto"><TextField source="Descripcion" /></ReferenceField>
            <ReferenceField source="NominaID" reference="Nomina"><TextField source="Descripcion" /></ReferenceField>
        </Datagrid>
    </List>
);


export const EmpleadoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="Cedula" />
            <TextInput source="Nombre" />
            <NumberInput source="Salario" />
            <ReferenceInput source="DepartamentoID" reference="Departamento"><SelectInput optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput source="PuestoID" reference="Puesto"><SelectInput optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput source="NominaID" reference="Nomina"><SelectInput optionText="Descripcion" /></ReferenceInput>
        </SimpleForm>
    </Edit>
);

export const EmpleadoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Cedula" />
            <TextInput source="Nombre" />
            <NumberInput source="Salario" />
            <ReferenceInput source="DepartamentoID" reference="Departamento"><SelectInput optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput source="PuestoID" reference="Puesto"><SelectInput optionText="Descripcion" /></ReferenceInput>
            <ReferenceInput source="NominaID" reference="Nomina"><SelectInput optionText="Descripcion" /></ReferenceInput>
        </SimpleForm>
    </Create>
);
