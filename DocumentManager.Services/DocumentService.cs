using DocumentManager.DAL.Model;
using DocumentManager.DAL.Repositories.Container;
using DocumentManager.DTO;
using DocumentManager.DTO.Models;
using DocumentManager.Infrastructure;
using DocumentManager.Infrastructure.Contracts;
using DocumentManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManager.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepositoryContainer _repositoryContainer;

        private readonly INotification _notification;

        public DocumentService(IRepositoryContainer repositoryContainer, INotification notification)
        {
            _repositoryContainer = repositoryContainer;
            _notification = notification;
        }

        public IEnumerable<DocumentModel> GetDocuments()
        {
            IEnumerable<DocumentModel> documents = _repositoryContainer.DocumentRepository.GetDocuments();

            return documents;
        }

        public long AddDocument(DocumentPostModel documentModel)
        {
            var document = new Document
            {
                FileName = documentModel.FileName,
                DocumentTypeID = documentModel.DocumentTypeID,
                FileTypeCode = documentModel.FileTypeCode,
                Comment = documentModel.Comment,
                Guid = Guid.NewGuid()
            };

            _repositoryContainer.DocumentRepository.Add(document);

            _repositoryContainer.SaveChanges();

            return document.DocumentID;
        }

        public DocumentEditModel GetDocumentForEdit(long documentId)
        {
            DocumentEditModel documentEditModel = _repositoryContainer
                .DocumentRepository
                .GetDocumentForEdit(documentId);

            return documentEditModel;
        }

        public DocumentEditResponseModel UpdateDocument(DocumentEditModel documentEditModel)
        {
            //_repositoryContainer.DocumentRepository.UpdateDocument(documentEditModel);
            _repositoryContainer.DocumentRepository.UpdateDocumentByAttach(documentEditModel);

            try
            {
                _repositoryContainer.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            DocumentEditResponseModel model = CreateReponseModel(documentEditModel);

            return model;
        }

        public bool DeleteDocument(long documentId)
        {
            Document document = _repositoryContainer.DocumentRepository.GetById(documentId);

            if (document == null)
            {
                throw new Exception("No database entry has been found");
            }

            _repositoryContainer.DocumentRepository.Delete(document);

            _repositoryContainer.SaveChanges();

            return true;
        }

        private DocumentEditResponseModel CreateReponseModel(DocumentEditModel documentEditModel)
        {
            var responseModel = new DocumentEditResponseModel();

            DocumentEditModel docEditModelFromDb = _repositoryContainer.DocumentRepository.GetDocumentForEdit(documentEditModel.DocumentID);

            responseModel.DatabaseValues = docEditModelFromDb;

            if (_notification.HasGeneralErrors &&
                _notification.GeneralErrors.Any(ge => ge.ErrorType == ExceptionType.ConcurrencyException))
            {
                responseModel.HasConcurrencyError = true;
                responseModel.ProposedValues = documentEditModel;
            }
            else
            {
                responseModel.HasConcurrencyError = false;
                responseModel.ProposedValues = null;
            }

            return responseModel;
        }
    }
}
