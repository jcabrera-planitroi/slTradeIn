using slTradeIn.Data;

namespace slTradeIn.Help
{

    public class BoxFlowItem
    {
        public int id;
        public int cartId;
        public string manufacturer;
        public string model;
        public string processorType;
        public string processorGen;
        public string memory;
        public string hdd;
        public string category;

        public decimal price;
        public int quantity;
        public bool dod;
        public decimal total;
        public string grade;
        public DateTime creationDate;

        public static BoxFlowItem FromCart(Dictionary<int, string> references, List<Ref_Cat> cats, List<Detail_ModelMaster> models, Detail_TTU_userCartDetail cart, DateTime dateCreated)
        {
            return new BoxFlowItem()
            {
                id = cart.iCartDetailID,
                cartId = cart.iCartID,
                manufacturer = references[cart.iManufacturer],
                model = models.First(x => x.iModelID == cart.iModelID).vModelNumber,
                processorType = references[cart.iProcessorType],
                processorGen = references[cart.iProcessorGen],
                memory = references[cart.iMemory],
                hdd = references[cart.iHDD],

                category = cats.First(x => x.iCatID == cart.iCategory).vCatDescription,
                price = cart.mPrice,
                quantity = cart.iQuantity,
                dod = cart.bDOD,
                total = cart.mTotal,
                grade = cart.vGrade,
                creationDate = dateCreated
            };
        }

    }
}