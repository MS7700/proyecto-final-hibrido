import * as React from "react";
import {
  List,
  Datagrid,
  TextField,
  BooleanField,
  Edit,
  Create,
  SimpleForm,
  NumberField,
  ReferenceField,
  NumberInput,
  ReferenceInput,
  SelectInput,
  DateField,
  DateInput,
  FormDataConsumer,
  RadioButtonGroupInput,
  Toolbar,
  ShowButton,
  TextInput,
  Show,
  SimpleShowLayout,
  BooleanInput,
  DeleteButton,
  TabbedShowLayout,
  Tab,
  ReferenceManyField,
  ExportButton,
} from "react-admin";
import { RaBox, BoxedShowLayout, ShowSplitter,RaGrid } from "ra-compact-ui";
import Typography from "@material-ui/core/Typography";

const Filters = [
  <NumberInput label="ID" source="id" />,
  <TextInput label="Nombre" source="Nombre" />,
  <DateInput source="Fecha" />,
  <TextInput label="Periodo" source="Periodo" />,
  <ReferenceInput
    label="Tipo de Nómina"
    source="TipoNominaID"
    reference="TipoNomina"
  >
    <SelectInput label="Tipo de Nómina" optionText="Descripcion" />
  </ReferenceInput>,
  <BooleanInput source="Contabilizado" />,
];

export const NominaList = (props) => (
  <List {...props} filters={Filters} sort={{ field: "Fecha", order: "DESC" }}>
    <Datagrid>
      <TextField source="id" />
      <DateField source="Fecha" />
      <TextField source="Periodo" />
      <ReferenceField
        label="Tipo de Nómina"
        source="TipoNominaID"
        reference="TipoNomina"
      >
        <TextField source="Descripcion" />
      </ReferenceField>
      <BooleanField source="Contabilizado" />
      <ShowButton />
      <DeleteButton />
    </Datagrid>
  </List>
);

const ShowToolBar = ({ basePath, data, resource }) => (
  <Toolbar>
    <DeleteButton basePath={basePath} record={data} resource={resource} />
  </Toolbar>
);

const AsideInfo = (props) => (
  <SimpleShowLayout {...props}>
    <Typography variant="h6">Nómina</Typography>
    <DateField source="Fecha" />
    <TextField source="Periodo" />
    <ReferenceField
      label="Tipo de Nómina"
      source="TipoNominaID"
      reference="TipoNomina"
    >
      <TextField source="Descripcion" />
    </ReferenceField>
    <BooleanField source="Contabilizado" />
    <DeleteButton />
  </SimpleShowLayout>
);

const ResumenTab = () => (
  <ReferenceManyField reference="NominaResumen" target="NominaID">
    <Datagrid>
      <ReferenceField label="Nómina" source="NominaID" reference="Nomina">
        <TextField source="Periodo" />
      </ReferenceField>
      <ReferenceField label="Empleado" source="EmpleadoID" reference="Empleado">
        <TextField source="Nombre" />
      </ReferenceField>
      <NumberField source="SueldoBruto" />
      <NumberField source="SueldoDevengado" />
    </Datagrid>
  </ReferenceManyField>
);

const DetalleTab = () => (
  <ReferenceManyField
    label="Nómina Resumen"
    reference="NominaResumen"
    target="NominaID"
  >
    <Datagrid>
            <ReferenceField
              label="Empleado"
              source="EmpleadoID"
              reference="Empleado"
            >
              <TextField source="Nombre" />
            </ReferenceField>
            <NumberField source="SueldoBruto" />
            <NumberField source="SueldoDevengado" />
          <ReferenceManyField
            label="Detalle"
            reference="NominaDetalle"
            target="NominaResumenID"
          >
            <Datagrid>
              <TextField source="Tipo" />
              <TextField source="Descripción" />
              <NumberField source="Monto" />
            </Datagrid>
          </ReferenceManyField>
    </Datagrid>
  </ReferenceManyField>
);

export const NominaShow = (props) => (
  <Show {...props} component="div" actions={<ExportButton />}>
    <ShowSplitter
      leftSideProps={{
        md: 2,
      }}
      rightSideProps={{
        md: 10,
      }}
      leftSide={<AsideInfo />}
      rightSide={
        <TabbedShowLayout>
          <Tab label="Resumido">
            <ResumenTab />
          </Tab>
          <Tab label="Detallado">
            <DetalleTab />
          </Tab>
        </TabbedShowLayout>
      }
    />
  </Show>
);

/*
    export const NominaShow = props => (
        <Show {...props}>
            <TabbedShowLayout>
                <Tab label="Enviar">
                <BoxedShowLayout>
                    <RaBox display="flex">
                        <RaBox display="flex" flexGrow={3} justifyContent="space-between" >
                            <DateField source="Fecha" />
                            <TextField source="Periodo" />
                            <ReferenceField label="Tipo de Nómina"  source="TipoNominaID" reference="TipoNomina"><TextField source="Descripcion" /></ReferenceField>
                            <BooleanField source="Contabilizado" />
                        </RaBox>
                        <RaBox display="flex" flexDirection="column" flexGrow={1}>
                            <TextField source="Fecha"/>
                        </RaBox>
                    </RaBox>
                </BoxedShowLayout>
                </Tab>
                <Tab label="Resumido">
                <ReferenceManyField label="Nómina Resumen" reference="NominaResumen" target="NominaID">
                    <Datagrid>
                    <TextField source="id" />
                    <ReferenceField label="Nómina" source="NominaID" reference="Nomina"><TextField source="Periodo" /></ReferenceField>
                    <ReferenceField label="Empleado" source="EmpleadoID" reference="Empleado"><TextField source="Nombre" /></ReferenceField>
                    <NumberField source="SueldoBruto" />
                    <NumberField source="SueldoDevengado" />
                    </Datagrid>
                </ReferenceManyField>
                </Tab>
                <Tab label="Detallado">
                    <ReferenceManyField label="Nómina Resumen" reference="NominaResumen" target="NominaID">
                        <Datagrid>
                        <TextField source="id" />
                        <ReferenceField label="Empleado" source="EmpleadoID" reference="Empleado"><TextField source="Nombre" /></ReferenceField>
                        <NumberField source="SueldoBruto" />
                        <NumberField source="SueldoDevengado" />
                        <ReferenceManyField label="Nómina Detalle" reference="NominaDetalle" target="NominaResumenID">
                            <Datagrid>
                                <TextField source="Tipo" />
                                <TextField source="Descripción" />
                                <NumberField source="Monto" />
                            </Datagrid>
                        </ReferenceManyField>
                        </Datagrid>
                    </ReferenceManyField>
                </Tab>
            </TabbedShowLayout>
        </Show>
    );
    */
export const NominaCreate = (props) => (
  <Create {...props}>
    <SimpleForm>
      <DateInput source="Fecha" />
      <ReferenceInput
        label="Tipo de Nómina"
        source="TipoNominaID"
        reference="TipoNomina"
      >
        <SelectInput label="Tipo de Nómina" optionText="Descripcion" />
      </ReferenceInput>
    </SimpleForm>
  </Create>
);
