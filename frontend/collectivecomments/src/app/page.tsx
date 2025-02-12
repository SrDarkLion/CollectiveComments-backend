import CreateCodeForm from "@/components/create-form"


export default function Home() {
  
  return (
    <div className="flex justify-center pt-5">
      <div className="mx-auto max-w-xl w-full px-4 sm:px-6 lg:px-8">
        <p className="text-center text-lg font-medium mb-8">Gerar Código FeedBack</p>
        <div className="mx-auto w-full">
          <CreateCodeForm/>
          <div className="sm:p-6 lg:p-8">
            <div className="flex justify-between items-end gap-3">
                <div className="w-full flex flex-col gap-2">
                  <label className="mt-1 font-semibold">Código Gerado</label>
                  <input
                    type="codefeedback"
                    className="w-full rounded-lg border h-11 border-gray-200 p-2  text-base shadow-xs"
                    disabled
                    value={'coca-2221'}
                  />
                </div>

                <button className="block w-20 rounded-lg bg-indigo-600 px-5 h-11 text-sm font-medium text-white">Copiar</button>
            </div>
            <p className="mt-1 text-red-500 font-semibold">Observação: Salvar código em um local seguro  </p>
              
          </div>
        </div>
      </div>

    </div>
  );
}
